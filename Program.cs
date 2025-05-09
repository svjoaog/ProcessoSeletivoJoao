using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

class Program
{
    class DiaFaturamento
    {
        public int dia { get; set; }
        public double valor { get; set; }
    }

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n=== MENU ===");
            Console.WriteLine("1 - Verificar número na sequência de Fibonacci (Questão 2)");
            Console.WriteLine("2 - Análise de faturamento (Questão 3)");
            Console.WriteLine("3 - Percentual de faturamento por estado (Questão 4)");
            Console.WriteLine("4 - Inverter palavra (Questão 5)");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
            
            string opcao = Console.ReadLine() ?? "";

            switch (opcao)
            {
                case "1":
                    VerificarFibonacciMenu();
                    break;
                case "2":
                    AnalisarFaturamento();
                    break;
                case "3":
                    FaturamentoPorEstado();
                    break;
                case "4":
                    InverterPalavra();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    static bool VerificarFibonacci(int n)
    {
        int a = 0, b = 1;
        while (a <= n)
        {
            if (a == n) return true;
            int temp = a;
            a = b;
            b = temp + b;
        }
        return false;
    }

    static void VerificarFibonacciMenu()
    {
        Console.Write("Digite o número: ");
        if (int.TryParse(Console.ReadLine(), out int numero))
        {
            if (VerificarFibonacci(numero))
                Console.WriteLine($"{numero} pertence à sequência de Fibonacci.");
            else
                Console.WriteLine($"{numero} não pertence à sequência de Fibonacci.");
        }
        else
        {
            Console.WriteLine("Entrada inválida.");
        }
    }

    

    static void AnalisarFaturamento()
    {
        try
        {
            string json = File.ReadAllText("dados.json");
            List<DiaFaturamento>? dados = JsonSerializer.Deserialize<List<DiaFaturamento>>(json);
            if (dados == null || dados.Count == 0)
                {
                    Console.WriteLine("Erro: dados.json está vazio ou mal formatado.");
                    return;
                }

            int numDias = dados.Max(d => d.dia);
            double[] vetor = new double[numDias];

            foreach (var item in dados)
                vetor[item.dia - 1] = item.valor;

            var (menor, maior) = FaturamentoMinMax(vetor);
            int diasAcimaMedia = DiasAcimaMedia(vetor);

            Console.WriteLine($"Menor faturamento: {menor} R$");
            Console.WriteLine($"Maior faturamento: {maior} R$");
            Console.WriteLine($"Dias com faturamento acima da média: {diasAcimaMedia} dias.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao ler ou processar o arquivo: {ex.Message}");
        }
    }

    static (double, double) FaturamentoMinMax(double[] vetor)
    {
        double menor = vetor.Where(v => v > 0).Min();
        double maior = vetor.Max();
        return (menor, maior);
    }

    static int DiasAcimaMedia(double[] vetor)
    {
        double soma = vetor.Where(v => v > 0).Sum();
        int diasComFaturamento = vetor.Count(v => v > 0);
        double media = soma / diasComFaturamento;

        return vetor.Count(v => v > media);
    }

    static void FaturamentoPorEstado()
    {
        var faturamento = new Dictionary<string, double>
        {
            { "SP", 67836.43 },
            { "RJ", 36678.66 },
            { "MG", 29229.88 },
            { "ES", 27165.48 },
            { "Outros", 19849.53 }
        };

        double total = faturamento.Values.Sum();

        foreach (var estado in faturamento)
        {
            double percentual = (estado.Value / total) * 100;
            Console.WriteLine($"{estado.Key}: {percentual:F2}%");
        }
    }

    static void InverterPalavra()
    {
        Console.Write("Digite a palavra: ");
        string palavra = Console.ReadLine() ?? "";

        char[] letras = palavra.ToCharArray();
        Array.Reverse(letras);
        string invertida = new string(letras);

        Console.WriteLine($"Palavra invertida: {invertida}");
    }
}
