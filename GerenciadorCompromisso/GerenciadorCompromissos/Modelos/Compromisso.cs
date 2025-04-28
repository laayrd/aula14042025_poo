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
    public string Hora 
    { 
        get { return _hora.ToString(); }
        set
        {
            _validarHoraInformada(value);
            _validarHoraValidaParaCompromisso();
        }
    }

    //public string HoraFormatada => _hora.ToString(@"hh\:mm");
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

    //private void _validarHoraInformada(TimeSpan hora) {}
    private void _validarHoraInformada(string hora)
    {
        if(!TimeSpan.TryParseExact(
            hora, @"hh\:mm",
            System.Globalization.CultureInfo.GetCultureInfo("pt-BR"),
            out _hora))
        {
            throw new Exception($"Hora {hora} inválida! Informe no formato HH:mm.");
        }
    }

    private void _validarHoraValidaParaCompromisso()
    {
        if (_hora < TimeSpan.FromHours(8))
        {
            throw new Exception($"Hora {_hora.ToString(@"hh\:mm")} é muito cedo para marcar compromisso. \nAgendamentos devem ser entre 08:00-12:00 ou 13:30-18:00.");
        }
        if (_hora >= TimeSpan.FromHours(12) && _hora < TimeSpan.FromHours(13.5))
        {
            throw new Exception($"Hora {_hora.ToString(@"hh\:mm")} é horário de almoço. \nAgendamentos devem ser entre 08:00-12:00 ou 13:30-18:00.");
        }
        if (_hora >= TimeSpan.FromHours(18))
        {
            throw new Exception($"Hora {_hora.ToString(@"hh\:mm")} é muito tarde para marcar compromisso. \nAgendamentos devem ser entre 08:00-12:00 ou 13:30-18:00.");
        }
        
    }
    public override string ToString()

    {
        return
               //$"Compromisso: {Descricao}\n" +
               $"Data: {Data}\n" +
               $"Hora: {Hora}\n";
               //$"Local: {Local}";
    }
}