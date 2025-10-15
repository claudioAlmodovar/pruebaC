using System;

namespace ApuestasApp.Models;

public class Bet
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string? Liga { get; set; }
    public string? Partido { get; set; }
    public decimal? Importe { get; set; }
    public decimal? Ganancia { get; set; }
    public string? Tipo { get; set; }
    public string? Resultado { get; set; }
    public decimal? Cuota { get; set; }
    public string? Nota { get; set; }
    public string? AntesDurante { get; set; }
    public string? Tipster { get; set; }
    public byte[]? Imagen { get; set; }
}
