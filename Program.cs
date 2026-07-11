using homework_dependancy_injection;


var serviceCollection = new CustomServiceCollection();
serviceCollection.AddTransient<INotificationService, EmailNotificationService>();
serviceCollection.AddTransient<NotificationService, NotificationService>();

serviceCollection.AddTransient<ITransientService, TransientService>();
serviceCollection.AddSingleton<ISingletonService, SingletonService>();


var serviceProvider = serviceCollection.BuildServiceProvider();


var TransientService1 = serviceProvider.GetRequiredService<ITransientService>();
var TransientService2 = serviceProvider.GetRequiredService<ITransientService>();

var SingletonService1 = serviceProvider.GetRequiredService<ISingletonService>();
var SingletonService2 = serviceProvider.GetRequiredService<ISingletonService>();

Console.WriteLine($"Transient Service 1 ID: {TransientService1.Id}");
Console.WriteLine($"Transient Service 2 ID: {TransientService2.Id}");
Console.WriteLine($"Singleton Service 1 ID: {SingletonService1.Id}");
Console.WriteLine($"Singleton Service 2 ID: {SingletonService2.Id}");