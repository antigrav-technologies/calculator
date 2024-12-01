using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace CalculatorBot;

internal class Program {
	private static readonly string TOKEN = File.ReadAllText("TOKEN.txt");

	private static readonly DiscordSocketClient CLIENT = new(new DiscordSocketConfig {
		GatewayIntents = GatewayIntents.All,
		UseInteractionSnowflakeDate = false
	});
	private readonly InteractionService _interactionService = new(CLIENT.Rest);

	public static Task Main() => new Program().MainAsync();
	private async Task MainAsync() {
		await _interactionService.AddModuleAsync<CommandModule>(null);
		CLIENT.Log += Log;
		CLIENT.Ready += Ready;
		CLIENT.SlashCommandExecuted += SlashCommandExecuted;
		//Client.SelectMenuExecuted += Client_SelectMenuExecuted;
		//Client.MessageReceived += Client_MessageReceived;
		//Client.ButtonExecuted += InteractionExecuted;
		_interactionService.Log += Log;
		await CLIENT.LoginAsync(TokenType.Bot, TOKEN);
		await CLIENT.StartAsync();
		await Task.Delay(-1);
	}

	private async Task<Task> SlashCommandExecuted(SocketSlashCommand cmd) {
		await _interactionService.ExecuteCommandAsync(new InteractionContext(CLIENT, cmd, cmd.Channel), null);
		return Task.CompletedTask;
	}

	private async Task<Task> Ready() {
		await _interactionService.RegisterCommandsGloballyAsync();
		//await ((ISocketMessageChannel)CLIENT.GetChannel(1307615346316021790)).SendMessageAsync("i am marilink");
		return Task.CompletedTask;
	}

	private static Task Log(LogMessage msg) {
		Console.WriteLine(msg);
		return Task.CompletedTask;
	}
}

internal class CommandModule : InteractionModuleBase {
	public required InteractionService Service { get; set; }

	[SlashCommand("ping", "ping")]
	public async Task PingSlashCommand() {
		await RespondAsync(((DiscordSocketClient)Context.Client).Latency + "ms");
	}

	[SlashCommand("add", "basic addition")]
	public async Task AddSlashCommand(double num1, double num2) {
		double sum = num1 + num2;
		if ((num1 == 9.0 && num2 == 10.0) || (num2 == 9.0 && num1 == 10.0)) sum = 21;
		await RespondAsync($"{num1} + {num2} = {sum}");
	}

	[SlashCommand("subtract", "basic subtraction")]
	public async Task SubtractSlashCommand(double num1, double num2) {
		await RespondAsync($"{num1} - {num2} = {num1 - num2}");
	}

	[SlashCommand("multiply", "basic multiplication")]
	public async Task MultiplySlashCommand(double num1, double num2) {
		await RespondAsync($"{num1} * {num2} = {num1 * num2}");
	}

	[SlashCommand("divide", "basic division")]
	public async Task DivideSlashCommand(double num1, double num2) {
		if (num2 == 0) {
			await RespondAsync("❌ Cannot divide by 0");
			return;
		}
		await RespondAsync($"{num1} / {num2} = {num1 / num2}");
	}

	[SlashCommand("power", "raise number to some power")]
	public async Task PowerSlashCommand(double num, double pow) {
		await RespondAsync($"{num}{pow.ToSuperscript()} = {Math.Pow(num, pow)}");
	}

	[SlashCommand("root", "get the number's root of some power")]
	public async Task RootSlashCommand(double num, double pow) {
		await RespondAsync($"{pow.ToSuperscript()}√{num} = {Math.Pow(num, 1f/pow)}");
	}

	[SlashCommand("gcd", "Greatest Common Divisor")]
	public async Task GcdSlashCommand(ulong a, ulong b) {
		await RespondAsync($"GCD({a}, {b}) = {GcdLcmUtils.Gcd(a, b)}");
	}

	[SlashCommand("lcm", "Least Common Multiple")]
	public async Task LcmSlashCommand(ulong a, ulong b) {
		await RespondAsync($"LCM({a}, {b}) = {GcdLcmUtils.Lcm(a, b)}");
	}
	
	[SlashCommand("random", "generate a random number")]
	public async Task RandomSlashCommand(int min, int max) {
		await RespondAsync($"rand({min}, {max}) = {XorShift64.NextInt32(min, max + 1)}");
	}
	
	[SlashCommand("log", "get a natural (base e) logarithm of a number")]
	public async Task LogSlashCommand(double num) {
		await RespondAsync($"log({num}) = {Math.Log(num)}");
	}
	
	[SlashCommand("log10", "get a base 10 logarithm of a number")]
	public async Task Log10SlashCommand(double num) {
		await RespondAsync($"log10({num}) = {Math.Log10(num)}");
	}
	
	[SlashCommand("log2", "get a base 2 logarithm of a number")]
	public async Task Log2SlashCommand(double num) {
		await RespondAsync($"log2({num}) = {Math.Log2(num)}");
	}
	
	[SlashCommand("sin", "get the sine of the an angle")]
	public async Task SinSlashCommand(double num) {
		await RespondAsync($"sin({num}) = {Math.Sin(num)}");
	}
	
	[SlashCommand("cos", "get the cosine of an angle")]
	public async Task CosSlashCommand(double num) {
		await RespondAsync($"cos({num}) = {Math.Cos(num)}");
	}
	
	[SlashCommand("tan", "get the tangent of an angle")]
	public async Task TanSlashCommand(double num) {
		await RespondAsync($"tan({num}) = {Math.Tan(num)}");
	}
	
	[SlashCommand("sigma", "aka euler function aka divisor function")]
	public async Task SigmaSlashCommand([MinValue(2)]ulong num) {
		await RespondAsync($"σ({num}) = {PrimeUtils.SigmaFunction(num)}");
	}

	[SlashCommand("factorial", "get factorial of a number")]
	public async Task FactorialSlashCommand(ulong num) {
		await RespondAsync($"{num}! = {FactorialUtils.Factorial(num)}");
	}
	
	[SlashCommand("fibonacci", "calculate the fibonacci sequence up to some point")]
	public async Task FibonacciSlashCommand([MinValue(1), MaxValue(100)] uint count) {
		await RespondAsync(string.Join(", ", FibonacciUtils.FibArray(count)));
	}

    [SlashCommand("factors", "get factors of the number")]
    public async Task FactorsSlashCommand([MinValue(2)]ulong num) {
		await RespondAsync($"{num} = {string.Join("\\*", PrimeUtils.Factors(num))}");
    }

    [SlashCommand("divisors", "get divisors of the number")]
    public async Task DivisorsSlashCommand(ulong num) {
        await RespondAsync(string.Join(", ", PrimeUtils.Divisors(num)));
    }

    [SlashCommand("number_info", "gives info about a (integer) number")]
	public async Task NumberInfoSlashCommand(long number) {
		string info = $"# {number} \n";
		info += "Primality: " + number switch {
			< 0 => "Negative number",
			0 => "0",
			1 => "1",
			_ when PrimeUtils.IsPrime((ulong)number) => "Prime",
			_ when PrimeUtils.IsPseudoprime((ulong)number) => "Composite, Pseudoprime",
			_ => "Composite"
		};
		await RespondAsync(info);
    }
}
