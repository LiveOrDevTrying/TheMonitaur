# **[The Monitaur](https://www.github.com/liveordevtrying/themonitaur)**
[The Monitaur](https://www.TheMonitaur.com) provides extremely easy logging and health monitoring services for your NTier layered applications.  [The Monitaur]{https://www.TheMonitaur.com} supports logging alerts through WebAPI, Tcp, and Websocket connections. It is easy to get started implementing The Monitaur in your .NET applications using the [provided .NET nuget packages](https://www.nuget.org/packages/themonitaur), but it is nearly as easy to write your own logic for interacting with the WebAPI, Tcp server, or Websocket servers. The Tcp and Websocket servers can support non-SSL or SSL connections, and acquiring your authorization / authentication token is as simple as registering a Client Application on The Monitaur's WebApp, copying the provided token, and attaching it to your payload - each protocol has a different way to submit your token, and those are details in more detail below. All [The Monitaur](https://www.TheMonitaur.com) packages referenced in this documentation are available on the NuGet package manager at in 1 aggregate package - [The Monitaur](https://www.nuget.org/packages/Tcp.NET/).

![Image of The Monitaur Logo](https://pixelhorrorstudios.s3-us-west-2.amazonaws.com/The+Monitaur/Landing/monitaur.png)

* [Getting Started](#gettingStarted)
* [Components](#components)
    * [Projects](#projects)
    * [Client Applications](#clientapplications)
    * [Alerts](#alerts)
* [Protocols](#protocols)
    * [WebAPI](#webapi)
    * [Websocket](#websocket)
    * [Tcp](#tcp)
    * [Rate Limiting](#rateLimiting)
* [Dashboard](#dashboard)
    * [Cards](#cards)
    * [Tables](#tables)
    * [Filters](#filters)
    * [Historic Records](#historicRecords)
* [Additional Information](#additional-information)

***

## **Getting Started**

To get started, first register for a free account at [The Monitaur.com](https://www.themonitaur.com). You can login using an existing account provided by: [Google](https://www.google.com), [Facebook](https://www.Facebook.com), [Twitch](https://www.twitch.tv), [Twitter](https://www.twitter.com), or [Microsoft](https://www.microsoft.com).

After you log in, you will be greated with a banner guiding you through creating your first [Project](#projects) and [Client Application](#clientapplications). First, input a name for your new Project (Figure 1). Secondly, input a name for your Client Application and choose an alert type of either: Card or Table (Figure 2). The next screen shows your new client application's token and offers you a chance to copy it to your clipboard. When you click close on the final screen, The Monitaur's dashboard will load and show your new Project and Client Application (Figure 3).

## **Components**

There are 3 main types of objects that are used within [The Monitaur](https://www.themonitaur.com):

* Projects
* Client Applications
* Alerts

### **Projects**

A project represents the top-most object you can create on [The Monitaur](https://www.themonitaur.com). A Project represents a collection of Client Applications that will be sending alerts to The Monitaur's servers. A new account created with The Monitaur initially allowed for 3 Projects total, but additional projects can be purchasaed from the Purchasable Items screen.

### **Client Applications**

A Client Application represents an object that will be submitting Alerts to The Monitaur's servers. Each Client Application will receive its own token. Rate limiting

***
