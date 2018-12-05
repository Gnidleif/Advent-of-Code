function part1(line)
    return reduce(line, 1)
end

function part2(line)
    local abet = get_alphabet()
    -- the length of the original line is as long as it's going to get
    local lowest = #line
    for _, char in pairs(abet) do
        local sub = remove_char(line, char)
        -- if nothing was removed, there's no need to reduce
        if #sub ~= #line then
            local num = part1(sub)
            if num < #sub and num < lowest then
                lowest = num
            end
        end
    end
    return lowest
end

function reduce(line, start)
    for i = start, #line-1 do
        -- string.byte("A") == 65, string.byte("a") == 97
        -- 97-65 == 32, 65-97 == -32
        local slice = line:sub(i,i+1)
        local res = slice:sub(1,1):byte() - slice:sub(2,2):byte()
        if res == 32 or res == -32 then
            -- splices the string to remove the reductionable pair
            -- note: if i-1 == 0 the function still works
            local tmp = string.format("%s%s", line:sub(1,i-1), line:sub(i+2))
            local idx = i-1
            if i == 1 then
                idx = 1
            end
            return reduce(tmp, idx)
        end
    end
    return #line
end

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
    if not file_exists(file) then return {} end
    lines = {}
    for line in io.lines(file) do
        lines[#lines+1] = line
    end
    return lines
end

function get_alphabet()
    local letters = {}
    for ascii = 97, 122 do
        table.insert(letters, string.char(ascii))
    end
    return letters
end

main()