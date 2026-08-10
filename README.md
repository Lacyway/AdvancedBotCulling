ABC adds a component that culls the rendering of bots that are outside of the default culling distance in EFT, helping older system to keep up.
-
### **Notice**

As of **SPT 3.10** this mod is no longer needed and as such I will no longer maintain it.

### **What does this mod do?** 

**ABC** adds a component to every bot that checks if they are outside of the base game culling radius. If they are outside of the radius, it completely disables the rendering of their bodies. This is a feature that is implemented in live when playing online that BSG for some reason decided not to add in offline mode apart from animation culling until version 0.15. **ABC** is an attempt to replicate what is done on live in offline mode by using the same methods.

### **How much of a difference in performance can I expect?**

On a modern system? Probably very little, if anything. This mod is not meant for high-end GPUs.

On an older system? You might see a minor performance increase when a lot of bots are alive.

This is not an "amazing wow fix" for Tarkov's performance issues, but it can make a difference on systems where the rendering is a bottleneck. I decided to implement it to see if it can help people with lower end PCs. This is designed to work with AI limiters.

### **Bug Reports** 

Please include your log file from %AppData%\\..\\LocalLow\\Battlestate Games\\EscapeFromTarkov\\
