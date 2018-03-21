# InterfaceLogger
Logger that is defined by interface


# le plan, ie. want

- reader for message sources (resx, json)
- validator for message sources (interface matches message source)
- generator for message sources (resx, json, .txt)
- https://github.com/damianh/LibLog
- injectables when a logging call is issued (FooMessage-inject: "Foo.Injection.Handler,BarAssembly" )
- named log parameters (FooMessage: "This is {test:1} message {for:0} participants")
- filter for parameters (FooMessage-filter: "{ $: 'exclude', 'user': 'i', 'domain': { 'name': 'i'}}")
  (ie. )
