import os

def part1(nums):
    return sum(nums)

def part2(nums):
    found = False
    current = 0
    freqs = {
        current: True
    }
    while(not found):
        for i in range(len(nums)):
            current += nums[i]
            if current in freqs:
                found = True
                break
            else:
                freqs[current] = True
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