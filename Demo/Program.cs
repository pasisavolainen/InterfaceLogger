using Demo;

SerilogHelper.SpinUpSerilog();


var logger = new DemoLoggirFactory().DemoLogger();

logger.TestWithDemoReturn()
    .TestWithParameters("hello");

