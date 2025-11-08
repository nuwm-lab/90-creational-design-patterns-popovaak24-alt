using System;

// Тема: Породжуючі патерни — Builder (Будівельник)
// Завдання: Реалізувати будівельник для створення комп'ютерної гри,
// який включає вибір графіки, звуку та сюжетної лінії.

class Game
{
	public string Graphics { get; set; }
	public string Sound { get; set; }
	public string Storyline { get; set; }

	public override string ToString()
	{
		return $"Game:\n  Graphics: {Graphics ?? "(none)"}\n  Sound: {Sound ?? "(none)"}\n  Storyline: {Storyline ?? "(none)"}";
	}
}

// Інтерфейс будівельника
interface IGameBuilder
{
	void Reset();
	void SetGraphics(string graphics);
	void SetSound(string sound);
	void SetStoryline(string storyline);
	Game GetGame();
}

// Конкретний будівельник для фентезійної гри
class FantasyGameBuilder : IGameBuilder
{
	private Game _game = new Game();

	public void Reset() => _game = new Game();

	public void SetGraphics(string graphics)
	{
		// за замовчуванням — фентезійна графіка, якщо не вказано інше
		_game.Graphics = graphics ?? "Піксельна фентезійна графіка";
	}

	public void SetSound(string sound)
	{
		_game.Sound = sound ?? "Оркестровий саундтрек";
	}

	public void SetStoryline(string storyline)
	{
		_game.Storyline = storyline ?? "Подорож героя: боротьба зі злом";
	}

	public Game GetGame()
	{
		var result = _game;
		Reset(); // готовий будувати новий об'єкт
		return result;
	}
}

// Конкретний будівельник для науково-фантастичної гри
class SciFiGameBuilder : IGameBuilder
{
	private Game _game = new Game();
	public void Reset() => _game = new Game();
	public void SetGraphics(string graphics)
	{
		_game.Graphics = graphics ?? "Реалістична 3D графіка у стилі кіберпанк";
	}
	public void SetSound(string sound)
	{
		_game.Sound = sound ?? "Електронний саундтрек з ефектами";
	}
	public void SetStoryline(string storyline)
	{
		_game.Storyline = storyline ?? "Колонізація космосу і штучний інтелект";
	}
	public Game GetGame()
	{
		var result = _game;
		Reset();
		return result;
	}
}

// Директор — визначає послідовність викликів будівельника для стандартних конфігурацій
class GameDirector
{
	private IGameBuilder _builder;

	public GameDirector(IGameBuilder builder)
	{
		_builder = builder;
	}

	public void SetBuilder(IGameBuilder builder)
	{
		_builder = builder;
	}

	// Побудувати мінімальну гру (лише графіка + звук)
	public void BuildMinimalViableProduct()
	{
		_builder.Reset();
		_builder.SetGraphics(null);
		_builder.SetSound(null);
		// storyline intentionally left unset (can be added later)
	}

	// Побудувати повнофункціональну гру (графіка + звук + сюжет)
	public void BuildFullFeaturedGame()
	{
		_builder.Reset();
		_builder.SetGraphics(null);
		_builder.SetSound(null);
		_builder.SetStoryline(null);
	}
}

class Program
{
	static void Main(string[] args)
	{
		Console.OutputEncoding = System.Text.Encoding.UTF8; // щоб українські символи коректно відображались

		// Демонстрація використання Builder
		var fantasyBuilder = new FantasyGameBuilder();
		var sciFiBuilder = new SciFiGameBuilder();

		var director = new GameDirector(fantasyBuilder);

		Console.WriteLine("-- Приклад 1: Фентезійна повнофункціональна гра (через Директора) --\n");
		director.BuildFullFeaturedGame();
		var fullFantasy = fantasyBuilder.GetGame();
		Console.WriteLine(fullFantasy);
		Console.WriteLine();

		Console.WriteLine("-- Приклад 2: Sci-Fi мінімальна гра (через Директора) --\n");
		director.SetBuilder(sciFiBuilder);
		director.BuildMinimalViableProduct();
		var minimalSciFi = sciFiBuilder.GetGame();
		Console.WriteLine(minimalSciFi);
		Console.WriteLine();

		Console.WriteLine("-- Приклад 3: Кастомна збірка (без Директора) --\n");
		// Можемо збирати крок за кроком, встановлюючи обрані опції
		var customBuilder = new FantasyGameBuilder();
		customBuilder.Reset();
		customBuilder.SetGraphics("Ручна ретро-піксельна графіка");
		customBuilder.SetSound("Чіптюн мелодії");
		// Нехай сюжет буде особливим
		customBuilder.SetStoryline("Мандрівка у світ загублених легенд");
		var customGame = customBuilder.GetGame();
		Console.WriteLine(customGame);
		Console.WriteLine();

		Console.WriteLine("-- Приклад 4: Комбінація Sci-Fi графіки з Фентезійним сюжетом --\n");
		// Ми можемо змішувати: використаємо SciFiBuilder, але задамо сюжет фентезі
		var mixBuilder = new SciFiGameBuilder();
		mixBuilder.Reset();
		mixBuilder.SetGraphics(null); // default Sci-Fi graphics
		mixBuilder.SetSound("Містичний хор у космосі");
		mixBuilder.SetStoryline("Фентезійна сага у майбутньому");
		var mixedGame = mixBuilder.GetGame();
		Console.WriteLine(mixedGame);

		Console.WriteLine();
		Console.WriteLine("Готово. Натисніть будь-яку клавішу для виходу...");
		Console.ReadKey();
	}
}
