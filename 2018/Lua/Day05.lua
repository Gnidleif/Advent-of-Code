function part1(line)
    return reduce(line, 1)
end

function part2(line)
    local lowest = #line
    while(coroutine.status(run_check) ~= "dead") do
        local _, num = coroutine.resume(run_check, line)
        if num ~= nil and num < lowest then
            lowest = num
        end
    end
    return lowest
end

function reduce(line, start)
    for i = start, #line-1 do
        -- string.byte("A") == 65, string.byte("a") == 97
        -- 97-65 == 32, 65-97 == -32
        local slice = line:sub(i,i+1)
        if math.abs(slice:sub(1,1):byte() - slice:sub(2,2):byte()) == 32 then
            -- splices the string to remove the reductionable pair
            -- note: if i-1 == 0 the function still works
            local idx = i-1
            local spliced = line:sub(1,idx) .. line:sub(i+2)
            if idx < 1 then
                idx = 1
            end
            return reduce(spliced, idx)
        end
    end
    return #line
end

local run_check = coroutine.create(function(str)
    local abet = get_alphabet()    
    -- the length of the original line is as long as it's going to get
    for _, char in pairs(abet) do
        local sub = remove_char(str, char)
        -- if nothing was removed, there's no need to reduce
        if #sub ~= #str then
            local num = reduce(sub, 1)
            if num < #sub then
                coroutine.yield(num)
            end
        end
    end
end)

function remove_char(line, char)
    -- pattern should be ['CHAR''char'] to find any occurence of the char
    local pattern = string.format("[%s%s]", string.upper(char), string.lower(char))
    -- if pattern doesn't match, just return the full string again
    if string.find(line, pattern) == nil then
        return line
    end
    return line:gsub(pattern, '')
end

function main()
    local line = read_file("Day05.txt")[1]
    --line = "dabAcCaCBAcCcaDA"
    print(part1(line))
    print(part2(line))
end

-- HELPER FUNCTIONS
function file_exists(file)
    local f = io.open(file, "rb")
    if f then 
        f:close()
    end
    return f ~= nil
end

function read_file(file)
    if not file_exists(file) then 
        return {} 
    end
    lines = {}
    for line in io.lines(file) do
        lines[#lines+1] = line
    end
    return lines
end

function get_alphabet()
    local letters = {}
    for ascii = 97, 122 do
        letters[#letters+1] = string.char(ascii)
    end
    return letters
end

main()