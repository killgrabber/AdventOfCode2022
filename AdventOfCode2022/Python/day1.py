import os

with open(os.path.join(os.sys.path[0], "../Inputs/input01.txt"), "r") as f:
    calories =f.readlines()

list = []
maxCa = 0
current = 0
for x in calories :
    try:
        current += int(x)
        #print(x)
    except:
        list.append(current)
        current = 0

list.sort()

print(sum([list.pop(), list.pop(), list.pop()]))