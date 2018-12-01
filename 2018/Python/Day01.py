import os

def part1(nums):
    return sum(nums)

def part2(nums):
    found = False
    current = 0
    freqs = {
        current: True
    }
    i = 0
    while(not found):
        current += nums[i % len(nums)]
        if current in freqs:
            found = True
        else:
            freqs[current] = True
        i += 1
    return current

def readFile(filename):
    path = os.path.abspath(__file__)
    scr_name = os.path.basename(__file__)
    with open(path.replace(scr_name, filename), 'r') as f:
        nums = f.read().splitlines()
    return [int(n) for n in nums]

if __name__ == "__main__":
    nums = readFile("Day01.txt")
    print(part1(nums))
    print(part2(nums))