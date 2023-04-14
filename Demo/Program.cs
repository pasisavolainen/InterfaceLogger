SerilogHelper.SpinUpSerilog();

var logger = new DemoLoggirFactory().DemoLogger();

logger.TestWithDemoReturn()
      .TestWithParameter("hello");

