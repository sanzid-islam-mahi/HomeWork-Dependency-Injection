using homework_dependancy_injection;


var serviceCollection = new CustomServiceCollection();
serviceCollection.AddTransient<INotificationService, EmailNotificationService>();
serviceCollection.AddTransient<NotificationService, NotificationService>();

serviceCollection.AddTransient<ITransientService, TransientService>();


var serviceProvider = serviceCollection.BuildServiceProvider();


var TransientService1 = serviceProvider.GetRequiredService<ITransientService>();
var TransientService2 = serviceProvider.GetRequiredService<ITransientService>();

Console.WriteLine($"Transient Service 1 ID: {TransientService1.Id}");
Console.WriteLine($"Transient Service 2 ID: {TransientService2.Id}");