import matplotlib.pyplot as plt
import numpy as np

def randomData():
    return np.cumsum(np.random.random_sample(100)-.5)

plt.figure(figsize=(6, 4))

axis1 = plt.gca()
axis1.set_ylabel("primary Y", color='b', fontsize=16)

for i in range(5):
    axis1.plot(randomData(), color='b')

axis2 = axis1.twinx()
axis2.set_ylabel("secondary Y", color='r', fontsize=16)
for i in range(5):
    axis2.plot(randomData()*1000, color='r')

plt.title("Multi-Y Demo", fontsize=24)
plt.tight_layout()
plt.savefig("multi-y.png")
plt.show()