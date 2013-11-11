ScriptCs.MPNSTester
===============================

[ScriptCS](https://github.com/scriptcs/scriptcs) script pack for testing Windows Phone 8  raw data push notification

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

```csharp
  // First require the MPNSTester
  > var tester = Require<MpnsTester>();
   
  // Choose the type of notification that you want to use (toast, tile or raw)
  > tester.RawDataSend("My Message","My push url");
   
```

   
