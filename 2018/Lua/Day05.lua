function part1(line, start)
    for i = start, #line-1 do
        local r = string.byte(line:sub(i,i)) - string.byte(line:sub(i+1,i+1))
        if r == 32 or r == -32 then
            local tmp = string.format("%s%s", line:sub(1,i-1), line:sub(i+2))
            local idx = i-1
            if i == 1 then
                idx = 1
            end
            return part1(tmp, idx)
        end
    end
    return #line
end

function part2(line)
    local abet = "abcdefghijklmnopqrstuvwxyz"
    local lowest = #line
    for i = 1, #abet do
        local num = part1(remove_char(line, abet:sub(i,i)), 1)
        if num < lowest then
            lowest = num
        end
    end
    return lowest
end

function remove_char(line, char)
    local pattern = string.format("[%s%s]", string.upper(char), string.lower(char))
    if string.find(line, pattern) == nil then
        return line
    end
    return line:gsub(pattern, '')
end

function file_exists(file)
    local f = io.open(file, "rb")
    if f then f:close() end
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

local line = read_file("Day05.txt")[1]
print(part1(line, 1))
print(part2(line))