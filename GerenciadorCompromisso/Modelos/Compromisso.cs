namespace ConsoleApp.Modelos;

public class Compromisso
{
    private DateTime _data;
    public string Data
    {
        get { return _data.ToString("dd/MM/yyyy"); }
        set
        {
            _validarDataInformada(value);
            _validarDataValidaParaCompromisso();
        }
    }
    private TimeSpan _hora;
    public TimeSpan Hora 
    { 
        get => _hora;
        set
        {
            _validarHoraInformada(value);
            _hora = value;
        }
    }

    public string HoraFormatada => _hora.ToString(@"hh\:mm");
    public string Descricao { get; set; }
    public string Local { get; set; }

    private void _validarDataInformada(string data) {
        if (!DateTime.TryParseExact(data,
                       "dd/MM/yyyy",
                       System.Globalization.CultureInfo.GetCultureInfo("pt-BR"),
                       System.Globalization.DateTimeStyles.None,
                       out _data))
        {
            throw new Exception($"Data {data} Inválida! Use o formato dd/MM/yyyy.");
        }
    }

    private void _validarDataValidaParaCompromisso() 
    {
        if (_data<=DateTime.Now) 
        {
            throw new Exception($"Data {_data:dd/MM/yyyy} já passou.");
        }
        if (_data<=DateTime.Now.AddMinutes(15))
        {
            throw new Exception("Agende com pelo menos 15 minutos de antecedência.");
        }
    }

    private void _validarHoraInformada(TimeSpan hora)
    {
        if (hora < TimeSpan.Zero || hora >= TimeSpan.FromHours(24))
        {
            throw new Exception($"Hora {hora} inválida! Informe no formato HH:mm entre 00:00 e 23:59");
        }
        bool horarioManha = hora >= TimeSpan.FromHours(8)    && hora < TimeSpan.FromHours(12);
        bool horarioTarde = hora >= TimeSpan.FromHours(13.5) && hora < TimeSpan.FromHours(18);

        if (!horarioManha && !horarioTarde)
        {
            throw new Exception($"Hora {hora} inválida! Agendamentos devem ser entre 08:00-12:00 ou 13:30-18:00.");
        }
    }
    public override string ToString()
    {
        return
               //$"Compromisso: {Descricao}\n" +
               $"Data: {Data}\n" +
               $"Hora: {HoraFormatada}\n";
               //$"Local: {Local}";
    }
}
