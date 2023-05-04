# <div align="center">VG Labs - Server Monitor System</div>
<div align="center">

![https://i.imgur.com/abaHWaq.png](https://i.imgur.com/abaHWaq.png)
#### by SemlerPDX
## Game and VOIP Server Monitoring & Public Server API Console App 
</div>

This is my (.NET Framework) console application (targeting .NET Framework 4.8) designed to monitor a game/voice server and contact the outside world under certain conditions, and soon, allow querying for this information from the outside world by 3rd party tools and apps, website/webpage systems, or even Discord bots.

As a background application, this server monitor application will perform alerts monitoring, performance & memory logging to CSV file, and other features listed below.  **This app will be configured to work out-of-the-box with Falcon BMS as the target, using BMS SharedMem library to access and make available information specific to BMS.**

To extend the usefulness of this application, I would like the open source app (while configured to start for Falcon BMS) to be organized and ambiguous enough to be tailored after a fashion to any retro game lacking proper query protocols for listing servers, and of course, take advantage of the included alerts systems for monitoring system memory usage, performance logging, or game/VOIP server crashes as needed.

### **Goals for Server Monitor System include:**
1.  Create our own game server query system for mods/games which lack it (i.e. Falcon BMS)
2.  Expose a public API for servers running SMS to allow querying data for use by apps, webpages, Discord bots, etc.
3.  Monitor game server & optional voice server processes for CTD (for games with no auto-restart capabilities)
4.  Monitor total server system memory usage (optional), log system memory data to CSV (also optional)
5.  Optional alerts system for game/voip CTD, excessive system memory use: log to file, SMTP email, Discord alert
6.  Create Open Source Discord Bot already set to query SMS server(s), allow setting game name, group name, etc.
7.  Provide Discord Bot as ready-to-use as well as open source codebase to get/write server info to Discord channel(s)
8.  Create sample webpage codebase showing how to use and display info from public API, share in documentation/repo
9.  Deploy release version of SMS, default/example settings tailored for Falcon BMS Servers
10. Employ 'mystery free' commenting & structures for easy refactoring for use with other retro game servers

### __**Current features:**__
- Game server crash alerts and email alerts when these modes are on
- VOIP server crash alerts and email alerts when these modes are on
- Excessive Memory Usage alerts and email alerts when these modes are on
- Current player count, callsigns, and status via BMS Shared Memory data
- Current server Theater of Operations name via BMS Shared Memory data
- System performance and memory data logging to CSV file at user defined frequency and duration
- User settings management through python format configuration file
- Command line argument overrides processing for any of these user settings
- Basic updates checking, writing any update information directly to the console

   

<div align="center"><br>

### ***Click link to DOWNLOAD, or clone the repository above & compile the app!***
__[https://veterans-gaming.com/semlerpdx/vglabs/apps/PLACEHOLDER/](https://veterans-gaming.com/semlerpdx/vglabs/apps/PLACEHOLDER/)__

#### Latest Changelog & Checksum:  [CLICK HERE (placeholder)](https://veterans-gaming.com/semlerpdx/vglabs/apps/PLACEHOLDER.html)

![https://i.imgur.com/CZEbmBd.png](https://i.imgur.com/CZEbmBd.png)
<br> **↑ update notice example shown above ↑** </br>
![https://i.imgur.com/grxBaoQ.png](https://i.imgur.com/grxBaoQ.png)

![https://i.imgur.com/1B5thJ8.png](https://i.imgur.com/1B5thJ8.png)
</div>

<div align="center" style="color:#8a0000">


### This app is in an Alpha Development as of March2023 - Please report any bugs or issues!
![https://i.imgur.com/kYtxqur_d.jpg?maxwidth=520&shape=thumb&fidelity=high](https://i.imgur.com/kYtxqur_d.jpg?maxwidth=520&shape=thumb&fidelity=high)
</div>

This section will expand in time as needed.

### __**Planned features:**__
- Public API which can be queried for server information
- Program usage guide, including all command line arguments and syntax
- Additional methods for retrieving server information from SharedMem
- Lootboxes (j/k lol)

____

**Questions I have for this Alpha Build:**

***Does this work and work well?  Should I improve or change anything (beyond the planned features above)?  Should I include any other features? Am I using dependency injection and interfaces properly? Am I being too anal with separation of concerns and class/method structure?  Have I done anything stupid, or made any mistakes?***
____

***Authors Note***

*As I wrote in my readme for the MouseMasterVR WPF app: I have past experience with C# and have toyed with a number of language, but this is my only my second real use of GitHub and my first console app - I'm writing in Visual Studio Community 2019, and I use PhotoShop for my images, and IcoFX for my icons.  While I'm as self-conscious as any self-taught coder new to something, I very much want feedback on my structure and use of methods as it relates to fundamentals, best practices, or common solutions in C#, and my attempt to follow a dependency injection and interface(s) structure, even my file/folder structures or naming choices.*

*I anticipate I may have made several odd choices and potentially non-standard methods, I can't know really as I am so new.  I have NEVER done this before on this scale, and started with a (mostly) blank Program.cs staring at me in February, and now here we are.  While I could have started with some console app framework, I felt it would be a more educational excercise to simply start with a new console app in VS and see where I landed after some coding.  I hope to continue making C# apps, and that this is just another of many such open source projects to come.*

*Thanks for checking out and helping with ServerMonitorSystem & providing any feedback!*
</div>
<div align="right">

*- SemlerPDX Mar-7-2023 -*</div>



---

### &nbsp; &nbsp; &nbsp; &nbsp; This freeware project is a product of several months of development and testing.


 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; [![Support me on Patreon](https://i.imgur.com/eUHzY5R.png)](https://www.patreon.com/SemlerPDX) &nbsp; &nbsp; &nbsp; [![Donate at PayPal](https://i.imgur.com/fgrCUPF.png)](https://veterans-gaming.com/semlerpdx/donate/) &nbsp; &nbsp; &nbsp; [![Buy me a Coffee](https://i.imgur.com/MkmhDDa.png)](https://www.buymeacoffee.com/semlerpdx)
 
 ## &nbsp; Gratuity is greatly appreciated and highly encouraging! Thank you!
 
---
