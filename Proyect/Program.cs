using System;
using System.Collections.Generic;

Dictionary<string, Dictionary<string, int>> clientes = new();

Console.WriteLine("Bienvenido");
MenuPrincipal();

void AgregarUsuario()
{
    Console.Write("Ingrese el número de cédula del cliente: ");
    string cedula = Console.ReadLine();
    Console.Write("Ingrese el estrato del cliente: ");
    int estrato = Convert.ToInt32(Console.ReadLine());
    Console.Write("Ingrese la meta de ahorro del cliente: ");
    int metadeahorro = Convert.ToInt32(Console.ReadLine());
    Console.Write("Ingrese el consumo actual del cliente: ");
    int consumoActual = Convert.ToInt32(Console.ReadLine());

    clientes[cedula] = new Dictionary<string, int>
        {
            {"estrato", estrato},
            {"meta_ahorro", metadeahorro},
            {"consumo_actual", consumoActual}
        };
    Console.WriteLine("Cliente agregado correctamente.");
}

void CalcularPrecioAPagar()
{
    Console.Write("Ingrese el número de cédula del cliente: ");
    string cedula = Console.ReadLine();
    if (clientes.ContainsKey(cedula))
    {
        Dictionary<string, int> cliente = clientes[cedula];
        int valorParcial = cliente["consumo_actual"] * 500;
        int valorIncentivo = (cliente["meta_ahorro"] - cliente["consumo_actual"]) * 500;
        int valorPagar = valorParcial - valorIncentivo;
        Console.WriteLine($"El valor a pagar del cliente {cedula} es: ${valorPagar}");
    }
    else
    {
        Console.WriteLine("Cliente no encontrado.");
    }
}

void CalcularPromedioDeConsumo()
{
    int totalConsumo = 0;
    foreach (var cliente in clientes.Values)
    {
        totalConsumo += cliente["consumo_actual"];
    }
    double promedio = (double)totalConsumo / clientes.Count;
    Console.WriteLine($"El promedio del consumo actual de energía es: {promedio} kilovatios");
}

void CalcularTotalDescuentos()
{
    int totalDescuentos = 0;
    foreach (var cliente in clientes.Values)
    {
        if (cliente["consumo_actual"] < cliente["meta_ahorro"])
        {
            totalDescuentos += (cliente["meta_ahorro"] - cliente["consumo_actual"]) * 500;
        }
    }
    Console.WriteLine($"El valor total de descuentos otorgados es: ${totalDescuentos}");
}

void MostrarPorcentajesDeAhorro()
{
    Dictionary<int, List<double>> porcentajesAhorro = new();
    foreach (var cliente in clientes.Values)
    {
        int estrato = cliente["estrato"];
        int consumoActual = cliente["consumo_actual"];
        int metaAhorro = cliente["meta_ahorro"];
        double porcentajeAhorro = ((double)(metaAhorro - consumoActual) / metaAhorro) * 100;

        if (porcentajesAhorro.ContainsKey(estrato))
        {
            porcentajesAhorro[estrato].Add(porcentajeAhorro);
        }
        else
        {
            porcentajesAhorro[estrato] = new List<double> { porcentajeAhorro };
        }
    }

    foreach (var kvp in porcentajesAhorro)
    {
        double promedioPorcentaje = kvp.Value.Sum() / kvp.Value.Count;
        Console.WriteLine($"El porcentaje de ahorro promedio para el estrato {kvp.Key} es: {promedioPorcentaje}%");
    }
}

void ContarCobrosAdicionales()
{
    int totalCobros = 0;
    foreach (var cliente in clientes.Values)
    {
        if (cliente["consumo_actual"] > cliente["meta_ahorro"])
        {
            totalCobros++;
        }
    }
    Console.WriteLine($"El número de clientes con cobro adicional es: {totalCobros}");
}

void MenuPrincipal()
{
    Console.WriteLine("¿Que Accion deseas realizar?");
    Console.WriteLine("1: Agregar un usuario Nuevo");
    Console.WriteLine("2: Buscar resultados por cédula");
    Console.WriteLine("0: Salir");
    Console.Write("Seleccione una opción: ");
    int opcion = Convert.ToInt32(Console.ReadLine());

    if (opcion == 0)
    {
        Console.WriteLine("Gracias por visitarnos. Hasta pronto");
    }
    else if (opcion == 1)
    {
        AgregarUsuario();
        MenuPrincipal();
    }
    else if (opcion == 2)
    {
        CalcularPrecioAPagar();
        CalcularPromedioDeConsumo();
        CalcularTotalDescuentos();
        MostrarPorcentajesDeAhorro();
        ContarCobrosAdicionales();
        MenuPrincipal();
    }
    else
    {
        Console.WriteLine("Algo salio mal.");
        MenuPrincipal();
    }

}