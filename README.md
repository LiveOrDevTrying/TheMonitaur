# **[The Monitaur](https://www.github.com/liveordevtrying/themonitaur)**<!-- omit in toc -->
[The Monitaur](https://www.themonitaur.com) provides easy logging and health monitoring services for any server-side application. [The Monitaur](https://www.themonitaur.com) supports logging [Alerts](#alerts) through [WebAPI](#webapi), [Tcp](#tcp), and [WebSocket](#websocket) connections and provides support for any secured application including perennial, distributed, NTier layered applications. It is easy to begin implementing [The Monitaur](https://www.themonitaur.com) in your own .NET applications using the [provided .NET nuget packages](https://www.nuget.org/packages/themonitaur), and it is nearly as easy to write your own logic for interacting with the [WebAPI](#webapi), [WebSocket](#websocket), or [Tcp](#tcp) servers for your server-side applications. [The Monitaur's](https://www.themonitaur.com) [Tcp](#tcp) and [WebSocket](#websocket) servers can support non-SSL or SSL connections, and [acquiring your authorization / authentication `token`](#client-authorization-token) is as simple as registering a [Client Application](#client-applications) on [The Monitaur's Web Application](https://app.themonitaur.com), copying the provided [`token`](#client-authorization-token), and attaching it to your payload. All [The Monitaur](https://www.themonitaur.com) packages referenced in this documentation are available on the [NuGet package manager](https://www.nuget.org) in 1 aggregate package - [The Monitaur](https://www.nuget.org/packages/themonitaur/).

[![Image of The Monitaur Logo](https://pixelhorrorstudios.s3-us-west-2.amazonaws.com/The+Monitaur/Github/LogoGithub.png)](https://www.themonitaur.com)

# **Getting Started**

To get started, first register for a free account at [The Monitaur.com](https://www.themonitaur.com). You can login using an existing account provided by: [Google](https://www.google.com), [Facebook](https://www.facebook.com), [Twitch](https://www.twitch.tv), [Twitter](https://www.twitter.com), or [Microsoft](https://www.microsoft.com).

After your initial log in, you will be greeted with a banner guiding you through creating your first [Project](#projects) and [Client Application](#client-applications). Input a name for your new [Project](#projects).

![Image welcome banner new Project](https://pixelhorrorstudios.s3-us-west-2.amazonaws.com/The+Monitaur/Github/Figure+1.PNG)

Next, input a name for your [Client Application](#client-applications) and choose an [Alert Type](#alertType) of either: [Card](#cards) or [Table](#tables).

![Image welcome banner new Client Application](https://pixelhorrorstudios.s3-us-west-2.amazonaws.com/The+Monitaur/Github/Figure+2.PNG)

The next screen shows your new [Client Application's](#client-applications) [`token`](#client-authorization-token) and offers you a chance to copy it to your clipboard. `This token is private and should not be publically exposed.` When you click close on the final screen, [The Monitaur.com's](https://www.themonitaur.com) dashboard will load and show your new [Project](#projects) and [Client Application](#client-applications).

![Image dashboard](https://pixelhorrorstudios.s3-us-west-2.amazonaws.com/The+Monitaur/Github/Figure+3.PNG)
***
# **Table of Contents**<!-- omit in toc -->
- [**Getting Started**](#getting-started)
- [**Components**](#components)
  - [**Projects**](#projects)
  - [**Client Applications**](#client-applications)
    - [**Client Authorization Token**](#client-authorization-token)
  - [**Alerts**](#alerts)
    - [**AlertType**](#alerttype)
    - [**StatusType**](#statustype)
    - [**Active and Dismissed**](#active-and-dismissed)
- [**Protocols**](#protocols)
  - [**WebAPI**](#webapi)
    - [**Manual Connections**](#manual-connections)
      - [**Authorization**](#authorization)
      - [**Endpoints**](#endpoints)
        - [**Get Client Application**](#get-client-application)
        - [**Get Alerts**](#get-alerts)
        - [**Get Alert**](#get-alert)
        - [**Create Alert**](#create-alert)
        - [**Dismiss Alerts**](#dismiss-alerts)
        - [**Delete Alert**](#delete-alert)
    - [**Nuget Packages**](#nuget-packages)
      - [**Parameters**](#parameters)
      - [**Methods**](#methods)
      - [**Update Your Token**](#update-your-token)
  - [**WebSocket**](#websocket)
    - [**Manual Connections**](#manual-connections-1)
      - [**Connect to the Server**](#connect-to-the-server)
      - [**Token**](#token)
      - [**SSL or Non-SSL**](#ssl-or-non-ssl)
      - [**Send an Alert**](#send-an-alert)
      - [**Ping**](#ping)
    - [**Nuget Packages**](#nuget-packages-1)
      - [**Parameters**](#parameters-1)
      - [**Methods**](#methods-1)
      - [**Ping**](#ping-1)
      - [**Dispose**](#dispose)
    - [**Example WebSocket HTML + JS Browser Client**](#example-websocket-html--js-browser-client)
  - [**Tcp**](#tcp)
    - [**Manual Connections**](#manual-connections-2)
      - [**Connect to the Server**](#connect-to-the-server-1)
      - [**Submit Your Token**](#submit-your-token)
      - [**SSL or Non-SSL**](#ssl-or-non-ssl-1)
      - [**Send an Alert**](#send-an-alert-1)
      - [**End-of-Line Characters**](#end-of-line-characters)
      - [**Ping**](#ping-2)
    - [**Nuget Packages**](#nuget-packages-2)
      - [**Parameters**](#parameters-2)
      - [**Methods**](#methods-2)
      - [**Ping**](#ping-3)
      - [**Dispose**](#dispose-1)
- [**Dashboard**](#dashboard)
  - [**Alert Cards**](#alert-cards)
  - [**Alert Tables**](#alert-tables)
  - [**Dismiss Alerts**](#dismiss-alerts-1)
  - [**Filters**](#filters)
  - [**Queries**](#queries)
  - [**Outputting Data**](#outputting-data)
  - [**Customize**](#customize)
- [**Additional Information**](#additional-information)

***

# **Components**

There are 3 types of objects that are used within [The Monitaur](https://www.themonitaur.com):

* [Projects](#projects)
* [Client Applications](#client-applications)
* [Alerts](#alerts)

***

## **Projects**

A [Project](#projects) represents the top-most object you can create on [The Monitaur](https://www.themonitaur.com). A [Project](#projects) represents a collection of [Client Applications](#client-applications) that will be sending [Alerts](#alerts) to [The Monitaur's](https://www.themonitaur.com) servers. `A newly created account allows for 3 Projects total, but additional projects can be purchased from the Purchasable Items screen.`

***

## **Client Applications**

A [Client Application](#client-applications) represents the entity that will be submitting [Alerts](#alerts) to [The Monitaur's servers](# "connect.themonitaur.com"). Each [Client Application](#client-applications) will receive its own [`token`](#client-authorization-token) which will be used for logging [Alerts](#alerts).\
\
**This [`token`](#client-authorization-token) should not be publically exposed.**\
\
The [Client Application](#client-applications) object is as follows:
```
interface IClientApplication
{ 
    "clientName": string,             // The Client Application Name
    "clientDescription": string       // The Client Application Description
}
```
In order to submit [Alerts](#alerts) on behalf of a [Client Application](#client-applications), the [Client Application's](#client-applications) [`token`](#client-authorization-token) must be submit on or following connection to [The Monitaur's servers](# "connect.themonitaur.com").

### **Client Authorization Token**
`To retrieve your Client Application's token`, log into [The Monitaur](https://www.themonitaur.com) and click on the [Projects](#projects) button the top navbar. 

![Image Projects menu item](https://pixelhorrorstudios.s3-us-west-2.amazonaws.com/The+Monitaur/Github/Figure+4.PNG)

Click on your parent [Project](#projects), and then click on your desired [Client Application](#client-applications).

![Image Client Applications registered to a project](https://pixelhorrorstudios.s3-us-west-2.amazonaws.com/The+Monitaur/Github/Figure+5.PNG)

 A password field is on the page that contains your [`token`](#client-authorization-token), and you can reveal or copy it to clipboard for use when submitting [Alerts](#alerts). 
 
![Image token for Client Application](https://pixelhorrorstudios.s3-us-west-2.amazonaws.com/The+Monitaur/Github/Figure+6.PNG)

 `This Client Application token is private and should not be publically available. This token is used to submit data to The Monitaur's servers, and if exposed, may result in unauthorized access and revocation of account services.`

A newly created account allows for 3 [Client Applications](#client-applications) per [Project](#projects), but additional [Client Applications](#client-applications) per [Project](#projects) can be purchasaed from the Purchasable Items screen.

***

## **Alerts**

An [Alert](#alerts) is an object sent by your registered [Client Application(s)](#client-applications) and logged at [The Monitaur](https://www.themonitaur.com). [Alerts](#alerts) can contain template information and are useful when evaluating and monitoring perennial distributed production applications. For example, an [Alert](#alerts) could be issued on a polling interval that discloses the current health of the application, or an [Alert](#alerts) could be sent to a WebAPI endpoint to ensure it remains online. [Alerts](#alerts) can also contain custom messages that can contain any data you like, and this could include custom status objects deserialized into a string and sent along with the [Alert](#alerts).

The [Alert](#alerts) object is as follows:
```
interface IAlert
{
    "id": number,                     // The Alert Id
    "statusType": number,             // The Status Type of the Alert
    "statusTypeValue": string,        // The Status Type of the Alert as a value
    "alertType": number,              // The Alert Type of the Alert
    "alertTypeValue": string,         // The Alert Type of the Alert as a value,
    "message": string,                // Custom string data you can include with the alert
    "timestamp": datetime             // The timestamp of the Alert adjusted to local time
}
```
### **AlertType**
[AlertType](#alertType) is an enum and has numeric values representing multiple states. You can use these values to represent any state, but it is recommended to think of [AlertType](#alertType) as a traditional Log Level.
```
enum AlertType {
    Debug,      // value: 0
    Info,       // value: 1
    Warning,    // value: 2
    Alert,      // value: 3
    Error       // value: 4
}
```
### **StatusType**
Status is an enum and has numeric values representing either Online or Offline.
```
enum StatusType {
    Online,     // value: 0
    Offline     // value: 1
}
```
### **Active and Dismissed**
[Alerts](#alerts) have 2 states: **Active**, and **Dismissed**. An [**Active**](#active-and-dismissed) [Alert](#alerts) has not yet been viewed and will be shown on [The Monitaur's Web Application](https://app.themonitaur.com) by default. After reviewing the [Alert(s)](#alerts), they can be [**Dismissed**](#active-and-dismissed) and archived. In this state, [Alerts](#alerts) are assumed to have been reviewed and are no longer displayed on the [Web Application](https://app.themonitaur.com). They can still be retrieved using [The Monitaur's WebAPI](https://api.themonitaur.com/swagger) and performing an [**Alerts Get**](#get-alerts), and specifying **IncludeDismissedAlerts=true** in the query parameters.

***

# **Protocols**

[The Monitaur](https://www.themonitaur.com) currently supports 3 types of connections for logging [Alerts](#alerts):

* [WebAPI](#webAPI)
* [Tcp](#tcp)
* [WebSocket](#websocket)

## **WebAPI**
You can connect to [The Monitaur's WebAPI](https://api.themonitaur.com/swagger) either [manually](#manual-connections) or you can use the provided [WebAPI Nuget Packages](#webapi-nuget-packages).

### **Manual Connections**
***
[The Monitaur](https://www.themonitaur.com) includes a [Swagger documented WebAPI](https://api.themonitaur.com/swagger) detailing the available WebAPI endpoints, requests, responses, and the capability to use test the WebAPI from the browser. Click on the Authorize in the upper-right hand corner of the [Swagger main page](https://api.themonitaur.com/swagger) and paste in the [`token`](#client-authorization-token) you previously retrieved for your [Client Application](#client-applications).

#### **Authorization**
In order to use the WebAPI, `you must include an authorization header to your request with the value as 'Bearer {token}', where {token} is the token generated for the desired Client Application.` [Please see above if you need help locating your token](#client-authorization-token).

#### **Endpoints**
6 secured WebAPI endpoints are included with [The Monitaur](https://www.themonitaur.com):

##### **Get Client Application**
* GET https://api.themonitaur.com/clientApplication
* Retrieve the [Client Application](#client-applications) registered to the [`token`](#client-authorization-token) included in the request header.
* Returns: **[IClientApplication](#client-applications)**

> curl -H "Authorization: bearer 3284283479255623" https://api.themonitaur.com/clientApplication

##### **Get Alerts**
* GET https://api.themonitaur.com/alerts
* Retrieve the undismissed Alerts registered to the [Client Application](#client-applications)
* Request: Query string parameters
    * **Max Records to Retrieve** - *number* - optional - The maximum number of Alerts to retrieve. Defaults to 150 and maximum is 50,000
    * **AlertTypes** - *array[AlertType]* - optional - The AlertType(s) to include with the response
    * **StatusTypes** - *array[StatusType]* - optional - The StatusType(s) to include with the response
    * **StartDate** - *string* - optional - The inclusive initial limit for querying Alerts in a date range
    * **EndDate** - *string* - optional - The exclusive ending limit for querying Alerts in a date range
    * **IncludeActiveAlerts** - *boolean* - optional - A flag to indiciate a request of Alerts that include those that have not been dismissed
    * **IncludeDismissedAlerts** - *boolean* - optional - A flag to indiciate a request of Alerts that include those that have been dismissed
* Returns: **array[[IAlert](#alerts)]**

> curl -H "Authorization: bearer 3284283479255623" https://api.themonitaur.com/alerts?maxRecordsToRetrieve=30000&alertTypes=0&alertTypes=1&alertTypes=2&statusTypes=0&startDate=2020-04-15T07:00:00.000Z&endDate=2020-05-15T07:00:00.000Z&includeActiveAlerts=true&includeDismissedAlerts=false

##### **Get Alert**
* GET https://api.themonitaur.com/alert/{id}
* Retrieve the specified [Alert](#alerts) registered to the authorized [Client Application](#client-applications)
* Request: Route parameter
    * **Id** - *number* - required - The Id of the [Alert](#alerts) to retrieve
* Returns: **[IAlert](#alerts)**

> curl -H "Authorization: bearer 3284283479255623" https://api.themonitaur.com/alert/5347

##### **Create Alert**
* POST https://api.themonitaur.com/alerts
* Create a new [Alert](#alerts) registered to the authenticated [Client Application](#client-applications)
* Request: Body
    *       interface IAlertCreateRequest {
                "statusType": number,
                "alertType": number,
                "message": string
            }
    * **StatusType** - *StatusType (number)* - required - The [StatusType](#statusType) to log with the [Alert](#alerts)
    * **AlertType** - *AlertType (number)* - required - The [AlertType](#alertType) to log with the [Alert](#alerts)
    * **Message** - *string* - optional - Any additional information (messages or serialized state data) to include with the [Alert](#alerts)
* Returns: **[IAlert](#alerts)**

> curl -X POST "https://api.themonitaur.com/Alerts" -H "Content-Type: application/json" -d "{\"statusType\":1,\"alertType\":4,\"message\":\"Hello world\"}" -H "Authorization: bearer 3284283479255623"

##### **Dismiss Alerts**
* POST https://api.themonitaur.com/alerts/dismiss
* Dismiss [Alert(s)](#alerts) registered to the authenticated [Client Application](#client-applications)
* Request: Body
    *       interface IAlertsDismissRequest {
                "ids": number[],
            }
    * **Ids** - *array[number]* - required - The Ids of the [Alert(s)](#alerts) requested to dismiss
* Returns: **Code 204**

> curl -X POST "https://api.themonitaur.com/Alerts/Dismiss" -H "Content-Type: application/json" -d "{\"ids\":[15,46,86]}" -H "Authorization: bearer 3284283479255623"

##### **Delete Alert**
* DELETE https://api.themonitaur.com/alerts/{id}
* Delete an [Alert](#alerts) registered to the authenticated [Client Application](#client-applications)
* **Note: This endpoint should be used as infrequently as possible**
* Request: Route parameter
    * **Id** - *number* - required - The Id of the [Alert](#alerts) requested to delete
* Returns: **Code 204**

> curl -X DELETE "https://api.themonitaur.com/Alerts/568" -H "Authorization: bearer 3284283479255623"

***
### **Nuget Packages**
A [WebAPI Client module](https://www.nuget.org/packages/TheMonitaur.WebAPI) is included and can be used to access [The Monitaur's WebAPI](https://api.themonitaur.com/swagger). First, install the [NuGet package](https://www.nuget.org/packages/TheMonitaur.WebAPI) using the [NuGet package manager](https://www.nuget.org):
> install-package TheMonitaur.WebAPI

This will add the most-recent version of the [The Monitaur WebAPI Module](https://www.nuget.org/packages/TheMonitaur.WebAPI) to your specified [Project](#projects).

Once installed, we can instantiate an instance of **IWebAPIClient** with the included implementation **WebAPIClient**. 
* `WebAPIClient(string token, string webAPIBaseUri = "https://api.themonitaur.com")`
    * An example instantiation is below:
```
IWebAPIClient client = new WebAPIClient(8943258989435839054532);
```  
#### **Parameters**
* **Token** - *string* - Required - Parameter containing the [`token`](#client-authorization-token) of the registered [Client Application](#client-applications) retrieved from [The Monitaur.com](https://www.themonitaur.com).
* **WebAPIBaseUri** - *string* - Optional - The endpoint / host / url of [The Monitaur's](https://www.themonitaur.com) WebAPI server instance to connect (defaults to [https://api.themonitaur.com](#)).

#### **Methods**
The following methods are exposed on IWebAPIClient:

* `Task<ClientApplicationDTO> GetClientApplicationAsync()`
  * Gets the [Client Application](#client-applications) that is registered to the [`Client Application Token`](#client-authorization-token)
* `Task<AlertDTO[]> GetAlertsAsync();`
  * Get the most recent 150 [undismissed](#active-and-dismissed) [Alerts](#alerts) for the [Client Application](#client-applications)
* `Task<AlertDTO[]> GetAlertsAsync(AlertsLookupRequest request);`
  * Get [Alerts](#alerts) for the [Client Application](#client-applications) conforming to the criteria in AlertsLookupRequest
  *     interface AlertsLookupRequest {
            "maxRecordsToRetrieve": number,
            "alertTypes": number[],
            "statusTypes": number[],
            "startDate": datetime,
            "endDate": dateTime,
            "includeActiveAlerts": boolean,
            "includeDismissedAlerts": boolean
        }
* `Task<AlertDTO> GetAlertAsync(long id);`
  * Get an [Alert](#alerts) registered to the [Client Application](#client-applications)
* `Task<AlertDTO> CreateAlertAsync(AlertCreateRequest request);`
  * Create a new [Alert](#alerts) registered to the [Client Application](#client-applications)
  *     interface AlertCreateRequest {
            "statusType": number,
            "alertType": number,
            "message": string
        }
* `Task<bool> DismissAlertsAsync(long[] ids);`
  * Dismiss the [Alerts](#alerts) registered to the [Client Application](#client-applications) with Ids contained in the ids input parameter
* `Task<bool> DeleteAlertAsync(long id);`
  * Delete the specified [Alert](#alerts) registered to the [Client Application](#client-applications)
  * **This endpoint should be used as infrequently as possible.**

#### **Update Your Token**
If you need to update the [`token`](#client-authorization-token) that you set in the constructor of **WebAPIClient**, you can use the included function:
* `void SetToken(string token)`

***

## **WebSocket**

You can connect to [The Monitaur's WebSocket servers](# "connect.themonitaur.com") either [manually](#manual-connections-2) or you can use the provided [WebSocket Nuget Packages](#websocket-nuget-packages).

***
### **Manual Connections**
#### **Connect to the Server**
To connect to [The Monitaur's WebSocket servers](# "https://connect.themonitaur.com"), you will need a [`token`](#client-authorization-token) for a registered [Client Application](#client-applications). To retrieve the [`token`](#client-authorization-token), please see [Retrieving Your Client Application Token](#retrieving-you-client-application-token).

Connect your WebSocket Client to the following endpoint and port. There are 2 ports to choose from - 1 is for an SSL secured WebSocket Server, the second is for an unsecured WebSocket Server. More information on the use of both servers is detailed in [SSL or Non-SSL](#websocket-ssl-or-non-ssl).

For the secured server:
```
Uri: wss://connect.themonitaur.com:6790/{token}
```
For the non-secured server: 
```
Uri: ws://connect.themonitaur.com:6795/{token}
```
#### **Token**
`You must send your token for your Client Application as a route parameter on the initial connection Uri in order to be authenticated on The Monitaur's Servers.` You do not need to add a prefix. An example of an initial connection Uri for the secured server is as follows:

> wss://connect.themonitaur.com:6790/659459068904568908434588847

If you connect successfully, you will receive a raw message back telling you that your connection to [The Monitaur](https://www.themonitaur.com) was successful.

#### **SSL or Non-SSL**
To enable SSL for your connection to [The Monitaur WebSocket servers](#websocket), you need to connect to port 6790. **It is recommended to use the SSL server for increased security**. You can however use the non-SSL server by connecting to port 6795. Both servers function identically, and because there is no encryption on the non-SSL server, the non-SSL server may have a slightly improved response rate.

#### **Send an Alert**
The **AlertCreateRequest** has the following signature:
```
interface AlertCreateRequest {
    "statusType": number,         // The Status Type of the Alert to create
    "alertType": number,          // The Alert Type of the Alert to create
    "message": string             // Custom message or serialized to include with the Alert
}
```
To send an [Alert](#alerts) to [The Monitaur's WebSocket servers](#websocket), create in JSON a new **AlertCreateRequest** and serialize it to a string. On your WebSocket Client that is connected to [The Monitaur's WebSocket servers](# "connect.themonitaur.com"), send the serialized data. An example of the serialized payload is below:

> socket.Send('{"statusType":0, "alertType": 1, "message": "Hello world"}')

#### **Ping**
[The Monitaur WebSocket servers](#websocket) will send a raw message containing **'ping'** to every client every 120 seconds to verify which connections are still alive. If a client fails to respond with a raw message containing **'pong'**, during the the next ping cycle, the connection will be severed and disposed. You should encorporate logic to listen for raw messages containing **'ping'**, and if received, immediately respond with a raw message containing the message **'pong'**. 

*Note: Failure to implement this logic will result in a connection being severed in up to approximately 240 seconds.*

***

### **Nuget Packages**
A [WebSocket Client module](https://www.nuget.org/packages/themonitaur.websocket) is included which can be used for non-SSL or SSL connections. To get started, first install the [NuGet package](https://www.nuget.org/packages/themonitaur) using the [NuGet package manager](https://www.nuget.org):
> install-package TheMonitaur.WebSocket

This will add the most-recent version of the [The Monitaur's WebSocket Module](https://www.nuget.org/packages/themonitaur.websocket) to your specified [Project](#projects). 

Once installed, we can instantiate an instance of **IMonitaurWebSocket** with the included implementation **MonitaurWebSocket**. 
* `MonitaurWebSocket(string token, string uri = "connect.themonitaur.com", int port = 6790, bool isSSL = true)`
    * An example instantiation is below:
```
IMonitaurWebSocket client = new MonitaurWebSocket(8943258989435839054532);
```  
#### **Parameters**
* **Token** - *string* - Required - Parameter containing the [`token`](#client-authorization-token) of the registered [Client Application](#client-applications) retrieved from [The Monitaur.com](https://www.themonitaur.com).
* **Uri** - *string* - Optional - The endpoint / host / url of [The Monitaur's](https://www.themonitaur.com) server instance to connect (defaults to [connect.themonitaur.com](#).
* **Port** - *int* - Optional - The port of [The Monitaur's](https://www.themonitaur.com) server instance to connect (e.g. 6790, 6795).
* **IsSSL** - *bool* - Optional - Flag specifying if the connection should be made using SSL encryption for the connection to the server.

#### **Methods**
1 method is exposed in [The Monitaur's WebSocket Module](#websocket):
* `Task SendAlertAsync(AlertCreateRequest request);`
    * Async method to send a new [Alert](#alerts) to [The Monitaur's](https://www.themonitaur.com) servers.

The **AlertCreateRequest** has the following signature:
```
interface AlertCreateRequest {
    "statusType": number,         // The Status Type of the Alert to create
    "alertType": number,          // The Alert Type of the Alert to create
    "message": string             // Custom message or serialized to include with the Alert
}
```
An example call to send a message to the server could be:
```
await client.SendAlertAsync(new AlertCreateRequest 
{
    "StatusType" = 2,
    "AlertType" = 1,
    "Message" = "Hello world"
});
```
#### **Ping**
If using [The Monitaur's WebSocket nuget packages](https://www.nuget.org/packages/themonitaur.websocket), **Ping** and **Pong** messages should be digested and handled automatically.

#### **Dispose**
At the end of usage, be sure to call `Dispose()` on the **IMonitaurWebSocket** object to free all allocated memory and resources.

***

### **Example WebSocket HTML + JS Browser Client**
Below is a simple Html Client that you can use to test [The Monitaur's WebSocket servers](#websocket). When implementing this test client, make sure to change '{token}' in the const uri to your desired [Client Application](#client-applications) [`token`](#client-authorization-token). \
\
**DO NOT USE THIS CLIENT IN PRODUCTION OR YOUR TOKEN WILL BE PUBLICALLY EXPOSED.**

```
<!DOCTYPE html>
<html>

<head>
	<meta charset="utf-8" />
	<title>Send An Alert To The Monitaur From WebSocket!</title>
</head>

<body>
    <h1>Send An Alert To The Monitaur From WebSocket!</h1>
	<input type=number id="alertType" placeholder="Alert Type"/>
	<input type=number id="statusType" placeholder="Status Type"/>
	<input type=text id="message" placeholder="Message"/>
	<button id="sendButton">Send</button>

	<ul id="messages"></ul>

    	<script language="javascript" type="text/javascript">
        const uri = "wss://connect.themonitaur.com:6790/{token}";
        function connect() {
            socket = new WebSocket(uri);
            socket.onopen = function(event) {
                console.log("opened connection to " + uri);
            };
            socket.onclose = function(event) {
                console.log("closed connection from " + uri);
            };
            socket.onmessage = function(event) {
                appendItem(list, event.data);
                console.log(event.data);

                if (event.data === 'Ping') {
                    sendMessage('Pong');
                }
            };
            socket.onerror = function(event) {
                console.log("error: " + event.data);
            };
        }
        connect();
        const list = document.getElementById("messages");
        const button = document.getElementById("sendButton"); 
        button.addEventListener("click", function() {
            
            const alertType = document.getElementById("alertType");
            const statusType = document.getElementById("statusType");
            const message = document.getElementById("message");
            sendMessage({ "alertType": alertType.value, "statusType": statusType.value, "message": message.value});
            
            alertType.value = "";
            statusType.value = "";
            message.value = "";
        });
        function sendMessage(message) { 
            socket.send(message);
            console.log(message);
        }
        function appendItem(list, message) {
            var item = document.createElement("li");
            item.appendChild(document.createTextNode(message));
            list.appendChild(item);
        }    
    </script>
</body>
</html>
```
***

## **Tcp**
You can connect to [The Monitaur's Tcp servers](# "connect.themonitaur.com") either [manually](#manual-connections-1) or you can use the provided [Tcp Nuget Packages](#tcp-nuget-packages).

### **Manual Connections**
***
#### **Connect to the Server**
To connect to [The Monitaur's Tcp Servers](# "connect.themonitaur.com"), you will need a [`token`](#client-authorization-token) for a registered [Client Application](#client-applications). To retrieve the [`token`](#client-authorization-token), please see [Retrieving Your Client Application Token](#retrieving-you-client-application-token).

Connect your Tcp Client to the following endpoint and port. There are 2 ports to choose from - 1 is for an SSL secured Tcp Server, the second is for an unsecured Tcp Server. More information on the use of both servers is detailed in [Tcp SSL or Non-SSL](#tcp-ssl-or-non-ssl).
```
Uri: connect.themonitaur.com
SSL Port: 6780
Non-SSL Port: 6785
End-of-Line Characters: \r\n
```
#### **Submit Your Token**
`You must send as the first message to the server a raw message containing your token. You must add **oauth:** as the prefix for your token.` This first message should look similar to the following:
> oauth:yourOAuthTokenGoesHere

If you connect successfully, you will receive a raw message back telling you that your connection to [The Monitaur](https://www.themonitaur.com) was successful.

#### **SSL or Non-SSL**
To enable SSL for your connection to [The Monitaur Tcp Server](#tcp), you need to connect to port 6780. **It is recommended to use the SSL server for increased security**. You can however use the non-SSL server by connecting to port 6785. Both servers function identically, and because there is no encryption on the non-SSL server, the non-SSL server may have a slightly improved response rate.

#### **Send an Alert**
The **AlertCreateRequest** has the following signature:
```
interface AlertCreateRequest {
    "statusType": number,         // The Status Type of the Alert to create
    "alertType": number,          // The Alert Type of the Alert to create
    "message": string             // Custom message or serialized to include with the Alert
}
```
To send an [Alert](#alerts) to [The Monitaur's Tcp servers](#tcp), create in JSON a new **AlertCreateRequest** and serialize it to a string. On your Tcp Client that is connected to [The Monitaur's Tcp servers](#tcp), send a UTF-8 byte array containing the serialized JSON data and the [end-of-line-characters](#end-of-line-characters), \r\n\\. An example of the serialized payload is below:

> socket.Send(Encoding.UTF8.GetBytes('{"statusType":0, "alertType": 1, "message": "Hello world"}\r\n'))

**Make sure to send the [End-of-Line characters](#end-of-line-characters) or your [Alert](#alerts) will not be written to the server**.

#### **End-of-Line Characters**
Tcp connections are persistent connections of streamed data. The server is unable to determine where breaks in the data exist without [End-Of-Line characters](#end-of-line-characters) to identify where to split apart the data. For [The Monitaur's](https://www.themonitaur.com) [end-Of-Line characters](#end-of-line-characters) are defined to be:

> \r\n

On many devices, this represents a **carriage return** and **new line**.

#### **Ping**
[The Monitaur Tcp servers](#tcp) will send a raw message containing **'ping'** to every client every 120 seconds to verify which connections are still alive. If a client fails to respond with a raw message containing **'pong'**, during the the next ping cycle, the connection will be severed and disposed. You will need to encorporate logic to listen for raw messages containing **'ping'**, and if received, immediately respond with a raw message containing the message **'pong'**. 

*Note: Failure to implement this logic will result in a connection being severed in up to approximately 240 seconds.*

***
### **Nuget Packages**
A [Tcp Client module](https://www.nuget.org/packages/themonitaur.tcp) is included which can be used for non-SSL or SSL connections. To get started, first install the [NuGet package](https://www.nuget.org/packages/themonitaur.tcp) using the [NuGet package manager](https://www.nuget.org):
> install-package TheMonitaur.Tcp

This will add the most-recent version of the [The Monitaur's Tcp Module](https://www.nuget.org/packages/TheMonitaur.Tcp) to your specified [Project](#projects). 

Once installed, we can instantiate an instance of **IMonitaurTcp** with the included implementation **MonitaurTcp**. 
* `MonitaurTcp(string token, string uri = "connect.themonitaur.com", int port = 6780, bool isSSL = true)`
    * An example instantiation is below:
```
IMonitaurTcp client = new MonitaurTcp(8943258989435839054532);
```  
#### **Parameters**
* **Token** - *string* - Required - Parameter containing the [`token`](#client-authorization-token) of the registered [Client Application](#client-applications) retrieved from [The Monitaur.com](https://app.themonitaur.com).
* **Uri** - *string* - Optional - The endpoint / host / url of [The Monitaur's Tcp Servers](https://www.themonitaur.com)(defaults to [connect.themonitaur.com](#)).
* **Port** - *number* - Optional - The port of [The Monitaur's](https://www.themonitaur.com) server instance to connect (e.g. 6780, 6785).
* **IsSSL** - *boolean* - Optional - Flag specifying if the connection should be made using SSL encryption for the connection to the server.

#### **Methods**
1 method is exposed in [The Monitaur's Tcp Module](#tcp):
* `Task SendAlertAsync(AlertCreateRequest request);`
    * Async method to send a new [Alert](#alerts) to [The Monitaur's Tcp Server](# "connect.themonitaur.com").

The **AlertCreateRequest** has the following signature:
```
interface AlertCreateRequest {
    "statusType": number,         // The Status Type of the Alert to create
    "alertType": number,          // The Alert Type of the Alert to create
    "message": string             // Custom message or serialized to include with the Alert
}
```
An example call to send a message to the server could be:
```
await client.SendAlertAsync(new AlertCreateRequest 
{
    StatusType = 2,
    AlertType = 1,
    Message = "Hello world"
});
```
#### **Ping**
If using [The Monitaur's Tcp nuget packages](https://www.nuget.org/packages/themonitaur.tcp), **Ping** and **Pong** messages should be digested and handled automatically.
   
#### **Dispose**
At the end of usage, be sure to call `Dispose()` on the **IMonitaurTcp** object to free all allocated memory and resources.

***

# **Dashboard**
[The Monitaur Web Application's](https://app.themonitaur.com) dashboard has been designed to provide tooling to maximize available screen-space for reviewing your [Alerts](#alerts). It features real-time updates, and as your [Client Applications](#client-applications) post Alerts to either [WebAPI](#webAPI), [WebSocket](#websocket), or [Tcp](#tcp), the [Web Application](https://app.themonitaur.com") will be automatically updated to reflect the new or updated [Alerts](#alerts). Additionally, all [filter settings](#filters) including but not limited to [Alert Types](#alertType), [Status Types](#statusType), [Client Applications](#client-applications), and [Projects](#projects) are saved between sessions, so you can quickly log into [The Monitaur](https://www.themonitaur.com) to review Alert notifications conforming to your set [Filters](#filters) and quickly [dismiss your reviewed Alerts](#dismiss-alerts).

The [dashboard](#dashboard) includes 2 different ways to visualize your Alerts:
* [Alert Cards](#alert-cards)
* [Alert Tables](#alert-tables)

*Note: At a later date, [The Monitaur](https://www.themonitaur.com) will support email notifications for selected [Alerts](#alerts). However, this feature is not yet ready for production.*
## **Alert Cards**
[Cards](#cards) are designed to be used when reviewing [Alerts](#alerts) or are browsing [The Monitaur](https://www.themonitaur.com) on a mobile device. There are 3 potential locations to interact with cards: on the Alert Cards page, on the [Client Application](#client-applications) dashboard, or after selecting [Alerts](#alerts) and clicking on **Review Selected Alerts**. Screenshots of these screens are included below.

![Image Review Alerts and Alert Cards screens](https://pixelhorrorstudios.s3-us-west-2.amazonaws.com/The+Monitaur/Github/Figure+9.png)

![Image Client Application dashboard with cards](https://pixelhorrorstudios.s3-us-west-2.amazonaws.com/The+Monitaur/Github/Figure+8.PNG)

## **Alert Tables**
[Tables](#tables) are designed to manage large number of [Alerts](#alerts). [Tables](#tables) can be accessed through the Alerts Table menu or by setting a [Client Application](#client-applications) to display on the dashboard as [tables](#tables). [Tables](#tables) allow for a quick overview of [undismissed](#dismiss-alerts) [Alerts](#alerts), a way to aggregately dismiss [Alerts](#alerts), or can be used to select [Alerts](#alerts) to further review. When you click on **Review Selected Alerts**, the next screen will populate with the selected [Alerts](#alerts) displayed as [Cards](#cards), and when you click on a [Card](#card), it will be [dismissed](#dismiss-alerts).

![Image Alert Tables screen](https://pixelhorrorstudios.s3-us-west-2.amazonaws.com/The+Monitaur/Github/Figure+9-0.png)

![Image Client Application dashboard tables](https://pixelhorrorstudios.s3-us-west-2.amazonaws.com/The+Monitaur/Github/Figure+10.PNG)

## **Dismiss Alerts**
You can [dismiss displayed Alerts](#active-and-dismissed) from the [Web Application](https://www.themonitaur.com). If the [Alerts](#alerts) are currently displayed as [Cards](#cards) (and is not a queried dataset), you can [dismiss any Alert](#dismiss-alerts) by clicking or tapping on it. If when looking at the [Client Application](#client-applications) dashboard component, you will see a modal showing you the [Alert](#alerts) in more detail with the **Dismiss Alert** as an action. [Dismissed Alerts](#dismiss-alerts) are not visible by default on the Web Application, but can be viewed and / or exported using [Queries](#queries) and [Outputting Data](#outputting-data).

## **Filters**
You can [Filter](#filters) [undismissed](#dismiss-alerts) [Alerts](#alerts) dynamically on the [Alerts Cards](#alerts-cards) / [Tables](#alerts-tables) screens or on any [Client Application](#client-applications) dashboard component. To [filter](#filters) [alerts](#alerts), first select **Set Filters** from the filter panel. The screen will refresh with [Alert Type](#alertType) and [Status Type](#statusType) expandable [filter](#filters) selections, and if in the [Alert Cards](#alert-cards) / [Table](#alert-tables) screen, a [Projects](#projects]) [filter](#filter) selection. Set the desired [filter](#filter) criteria including the shown [Alert Types](#alertType), [Status Types](#statusType), and / or [Projects](#projects) / [Client Applications](#client-applications), and the [Alert Card](#alert-cards) or [Table](#alert-tables) component will update immediately.

**If the data expected onscreen is not being shown, it more than likely is caused by [Filters](#filters). [Filters](#filters) retain their state (displayed or not displayed) between your login sessions. You can reset the [Filters](#filters) by selecting `Reset Filters` on the respective [Filter](#filters) panel.**

## **Queries**
There are 2 ways to [query historic alerts](#queries): [Alert Cards](#alert-cards) or [Alert Table](#alert-tables) screens, and any [Client Application](#client-applications) dashboard.

To query historic records, first navigate to either the [Alerts Cards](#alert-cards) / [Table](#alert-tables) screens or any [Client Application](#client-applications) [dashboard](#dashboard) component. Now click on the checkbox that says **Query History Alerts**. 

![Image Customize account Alert notifcation colors for text and background](https://pixelhorrorstudios.s3-us-west-2.amazonaws.com/The+Monitaur/Github/Figure+11.PNG)

The [Filters](#filters) panel will change into a [Query panel](#queries). From this panel, you can select a desired start and end date, a desired number of records, to include active records, to include dismissed records, select included [Alert Types](#alertType), and select included [Status Types](#statusType). Modify the query parameters to match the desired query, and then click **Submit Filter and Query**. The [Query panel](#queries) will become locked and the queried results will be displayed. Queried records (if active) cannot be dismissed from when the [Query panel](#queries) is active.

## **Outputting Data**
Data that has been [Queried](#queries) can be exported as a Comma Separated Value (CSV) file. To export [Queried data](#queries), when the desired [Queried data](#queries) is displayed on screen by querying for [Alerts](#alerts), click on the **Export Queried Alerts** button.

![Image export queried Alerts](https://pixelhorrorstudios.s3-us-west-2.amazonaws.com/The+Monitaur/Github/Figure+13.png)

The maximum number of rows you can query using [The Monitaur's Web Application](https://www.themonitaur.com) is 50,000 records per request. If you require more than 50,000 records, you can modify the query parameters to return sub-queries and union them on your local machine.

## **Customize**
[Alerts](#alerts) can have their colors customized by [Alert Type](#alertType) and [Status Type](#statusType) by clicking on the **Accounts** menu item. 

![Image Customize account Alert notifcation colors for text and background](https://pixelhorrorstudios.s3-us-west-2.amazonaws.com/The+Monitaur/Github/Figure+12.png)

To setup customized colors, select the [Alert Type](#alertType) you would like to customize, and choose a Text and / or Background Color from the Color Picker. The displayed records in the dashboard will automatically update to reflect the color changes.

***

# **Additional Information**
[The Monitaur](https://www.themonitaur.com) was created by [LiveOrDevTrying](https://www.liveordevtrying.com) and is maintained by [Pixel Horror Studios](https://www.pixelhorrorstudios.com). [The Monitaur](https://www.themonitaur.com) is currently implemented in (but not limited to) the following projects: [Allie.Chat](https://allie.chat), [There Is No Turning Back!](https://noturningbackgame.com), and [OpenDJRadio](https://opendjradio.com).  \
\
![Pixel Horror Studios Logo](https://pixelhorrorstudios.s3-us-west-2.amazonaws.com/Packages/PHS.png)