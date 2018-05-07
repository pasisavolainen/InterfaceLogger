# InterfaceLogger
Logger that is defined by interface.

## Just... Why

Log messages are generally semantic stuff (we're here in the code and thing x-y-z happened, we had a-b-c stuff in hand at that moment, there was p-q-r context at that time).

Text, log level and interpretation of what-happened, parameters-in-hand or context don't really belong into that point of code. 
Providing them to logger does, but not defining what exactly they are and how they should be shown to end user/debugger/tester.

Typically logging is also heavily misapplied. Programmer/debug logging is conflated with actual "Business logic" logging.
With interface defined logging it is extremely simple to make sure that "customer" logs only have messages that you want them to have.
So your application can have `IPaymentAuditLog` interface that will only ever contain messages that can appear on the payment audit 
log and there is no way that any other messages would appear there through logging subsystem.

# Usage

Create an interface that has the messages you want to write as methods:
```C#
// /!\ interface must be `public` or you must have 
//     [assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")] on that assembly.
public interface ICustomerLog {
    void ReceiptReceived(DateTime dt);
    void PaymentCancelled(DateTime dt, string reason);
}

...

// Create the logger from the interface
// LogContext is the context of the logger, typically like log4net's LogManager.GetLogger(typeof(LogContext))
// ICustomerLog is the interface you defined
// LogResource is a resx file you have
var logger = InterfaceLogger.LoggerManager.Get<LogContext, ICustomerLog>(LogResource.ResourceManager);

// Use logger:
logger.ReceiptReceived(DateTime.UtcNow);
```

# ResX message source

- Messages in resx file should be 1:1 same as they are in the interface.
- Message level can be overridden (default `Info` by having resource string for the message with appended `_` character, values can be `verbose, debug, info, warn, error, fatal`)
  - so for example message `FatalMessage` would also have `FatalMessage_` resource string with `fatal` as value to make that message fatal.
- It doesn't matter if resx contains extra messages.

| Name              | Value                                   |
| ----------------- | --------------------------------------- |
| Message           | Plain message without specific settings |
| MessageWithLevel  | Message text                            |
| MessageWithLevel_ | FATAL                                   |
    

# Message parameters

Standard C# string interpolation is used, ie. first parameter is `{0}` and the string formatting codes are also used.

# le plan, ie. want

- reader for message sources (json)
- validator for message sources (interface matches message source)
  - also test drive the message source (especially positional string interpolation parameters)
- generator for message sources (resx, json, .txt)
- [X] https://github.com/damianh/LibLog
  - [ ] unmake it suck:
    - [ ] use correct context (hardcoded to `ISink`)
    - [ ] nested contexts
- injectables when a logging call is issued (FooMessage-inject: "Foo.Injection.Handler,BarAssembly" )
- named log parameters (FooMessage: "This is {test:1} message {for:0} participants")
  - [x] parameter name info is available through the proxy/fake library
- filter for parameters (FooMessage-filter: "{ $: 'exclude', 'user': 'i', 'domain': { 'name': 'i'}}")
  (ie. )
