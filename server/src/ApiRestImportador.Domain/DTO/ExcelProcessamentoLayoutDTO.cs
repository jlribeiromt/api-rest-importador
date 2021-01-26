namespace ApiRestImportador.Domain.DTO
{
    public class ExcelProcessamentoLayoutDTO
    {
        public ExcelProcessamentoLayoutDTO(int linha,
                                           int coluna,
                                           string dysplayName,
                                           string valor,
                                           object tipo,
                                           bool itemVazio)
        {
            Linha = linha;
            Coluna = coluna;
            DysplayName = dysplayName;
            Valor = valor;
            Tipo = tipo;
            ItemVazio = itemVazio;
        }

        public int Linha { get; private set; }
        public int Coluna { get; private set; }
        public string DysplayName { get; private set; }
        public string Valor { get; private set; }
        public object Tipo { get; private set; }
        public bool ItemVazio { get; private set; }
    }
}
