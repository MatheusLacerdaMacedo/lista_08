namespace BibliotecaApi.Models;

public class Biblioteca
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Inicio_funcionamento { get; set; }
    public string? Fim_funcionamento { get; set; }
    public int inauguracao { get; set; }
     public string? contato { get; set; }
}
