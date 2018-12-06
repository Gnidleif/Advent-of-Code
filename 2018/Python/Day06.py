import os, math

class Area:
    def __init__(self, p):
        self.pos = p
        self.valid = True
        self.count = 0

def distanceFrom(a, b):
    return abs(a[0]-b[0]) + abs(a[1]-b[1])

def inBounds(p, bounds):
    return (p[0] > bounds["min"][0] and p[0] < bounds["max"][0] and p[1] > bounds["min"][1] and p[1] < bounds["max"][1])

def calcBounds(points):
    x_coords, y_coords = zip(*points)
    return {"min": [min(x_coords), min(y_coords)], "max": [max(x_coords), max(y_coords)]}

def part1(areas, bounds):
    for x in range(bounds["min"][0], bounds["max"][0]+1):
        for y in range(bounds["min"][1], bounds["max"][1]+1):
            idx = -1
            low = math.inf
            current = [x,y]
            for i in range(len(areas)):
                dist = distanceFrom(current, areas[i].pos)
                if dist == low:
                    idx = -1
                elif dist < low:
                    low = dist
                    idx = i

            if idx >= 0:
                areas[idx].count += 1
                if not inBounds(current, bounds):
                    areas[idx].valid = False

    return max([a.count for a in areas if a.valid])

def part2(areas, bounds, limit):
    valid = 0
    for x in range(bounds["min"][0], bounds["max"][0]+1):
        for y in range(bounds["min"][1], bounds["max"][1]+1):
            total = 0
            current = [x,y]
            for i in range(len(areas)):
                total += distanceFrom(current, areas[i].pos)
                if total >= limit:
                    break
            if total < limit:
                valid += 1

    return valid

def readAreas(filename):
    return [Area(p) for p in [[int(lst[0]), int(lst[1])] for lst in [line.split(", ") for line in open("Day06.txt", 'r').read().splitlines()]]]

if __name__ == "__main__":
    areas = readAreas("Day06.txt")
    bounds = calcBounds([a.pos for a in areas])

    print(part1(areas, bounds))
    print(part2(areas, bounds, 10000))