ScriptCs.MPNSTester
===============================

[ScriptCS](https://github.com/scriptcs/scriptcs) script pack for testing Windows Phone 8  push notification system. Choose between tile, toast or raw notification.

## Installation

Simply type:

    scriptcs -install ScriptCs.MPNSTester

Or create packages.config:

    <?xml version="1.0" encoding="utf-8"?>
    <packages>
        <package id="ScriptCs.MPNSTester" targetFramework="net45" />
    </packages>

And run:

    scriptcs -install
    
This will install `ScriptCs.MPNSTester` and the necessary dependencies and copy them to a `bin` folder relative to the place from where you executed the installation.

## Usage

Once you are at the REPL prompt you have just to require the tester and choose the type of notification that you want to test (toast, tile or raw data): 
```csharp
  // First require the MPNSTester
  > var tester = Require<NotificationManager>();
   
  // Choose the type of notification that you want to use (toast, tile or raw)
  > tester.RawDataSend("My Message","My push url");
  
   
```

   
