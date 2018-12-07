import os, math

class Area:
    def __init__(self, p):
        self.pos = p
        self.infinite = False
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
                    areas[idx].infinite = True

    return max([a.count for a in areas if not a.infinite])

def part2(areas, bounds, limit):
    points = set()
    for x in range(bounds["min"][0], bounds["max"][0]+1):
        for y in range(bounds["min"][1], bounds["max"][1]+1):
            total = 0
            current = [x,y]
            for i in range(len(areas)):
                total += distanceFrom(current, areas[i].pos)
                if total >= limit:
                    break
            if total < limit:
                points.add("{},{}".format(current[0], current[1]))

    return len(points)

def readPoints(filename):
    with open(filename, 'r') as f:
        lines = f.read().splitlines()

    return [Area([int(s[0]), int(s[1])]) for s in [line.split(", ") for line in lines]]

if __name__ == "__main__":
    areas = readPoints("Day06.txt")
    bounds = calcBounds([a.pos for a in areas])

    print(part1(areas, bounds))
    print(part2(areas, bounds, 10000))