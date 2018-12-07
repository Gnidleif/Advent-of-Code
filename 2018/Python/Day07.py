data = """Step C must be finished before step A can begin.
Step C must be finished before step F can begin.
Step A must be finished before step B can begin.
Step A must be finished before step D can begin.
Step B must be finished before step E can begin.
Step D must be finished before step E can begin.
Step F must be finished before step E can begin.""".splitlines()

def readFile(filename):
    return [line for line in open(filename, 'r').read().splitlines()]

def filterLines(lines):
    return [[word[1], word[7]] for word in [line.split(' ') for line in lines]]

def buildQueue(arr):
    queue = {}
    for i in range(len(arr)):
        key = arr[i][1]
        val = arr[i][0]
        if key not in queue:
            queue[key] = set()
        queue[key].add(val)
    return queue

def finish(queue):
    keys = set(queue.keys())
    items = set()
    for k, v in queue.items():
        items = items.union(v)
    return sorted(list(items - keys))[0]

def doWork(queue, current):
    toPop = []
    for k, v in queue.items():
        queue[k] = v - set(current)
        if len(queue[k]) == 0:
            toPop.append(k)
    for i in range(len(toPop)):
        queue.pop(toPop[i])
    return toPop

def part1(queue):
    result = []
    while len(queue) > 0:
        done = finish(queue)
        result.append(done)
        active = doWork(queue, done)
        if len(queue) == 0:
            result.extend(active)
    return ''.join(result)

queue = buildQueue(filterLines(readFile("Day07.txt"))) # AFBDE
print(part1(queue))