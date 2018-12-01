import os

def part1(nums):
    return sum(nums)

def part2(nums):
    current = 0
    freqs = set()
    freqs.add(current)
    i = 0
    while(len(freqs) > i):
        current += nums[i % len(nums)]
        freqs.add(current)
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