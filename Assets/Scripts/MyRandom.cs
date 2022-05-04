using Random = System.Random;

public static class MyRandom {
    private static readonly Random _random = new Random();

    public static int GetRandomMultiplicatorValue() {
        return 1 + _random.Next(16);
    }
}