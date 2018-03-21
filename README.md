# InterfaceLogger
Logger that is defined by interface

# Usage

Create an interface that has the messages you want to write as methods:

    // /!\ interface must be `public` or you must have `[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]` on that assembly.
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

# ResX message source

- Messages in resx file should be 1:1 same as they are in the interface.
- Message level can be overridden (default `Info` by having resource string for the message with appended `_` character, values can be `verbose, debug, info, warn, error, fatal`)
  - so for example message `FatalMessage` would also have `FatalMessage_` resource string with `fatal` as value to make that message fatal.
- It doesn't matter if resx contains extra messages.
    

# Message parameters

Standard C# string interpolation is used, ie. first parameter is `{0}` and the string formatting codes are also used.

# le plan, ie. want

- reader for message sources (json)
- validator for message sources (interface matches message source)
  - also test drive the message source (especially positional string interpolation parameters)
- generator for message sources (resx, json, .txt)
- https://github.com/damianh/LibLog
- injectables when a logging call is issued (FooMessage-inject: "Foo.Injection.Handler,BarAssembly" )
- named log parameters (FooMessage: "This is {test:1} message {for:0} participants")
- filter for parameters (FooMessage-filter: "{ $: 'exclude', 'user': 'i', 'domain': { 'name': 'i'}}")
  (ie. )
