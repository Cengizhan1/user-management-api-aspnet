using UserWorkerService;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.Configure<RabbitMqInfo>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
