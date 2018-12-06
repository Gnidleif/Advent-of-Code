function part1(nums)
    local sum = 0
    for _, num in pairs(nums) do
        sum = sum + num
    end
    return sum
end

function part2(nums)
    local current = 0
    local seen = {current = true}
    local idx = 0
    while true do
        current = current + nums[(idx % #nums)+1]
        idx = idx + 1
        if seen[current] == nil then
            seen[current] = true
        else
            break
        end
    end
    return current
end

function main()
    local lines = read_file("Day01.txt")
    print(part1(lines))
    print(part2(lines))
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
        lines[#lines+1] = tonumber(line)
    end
    return lines
end

main()