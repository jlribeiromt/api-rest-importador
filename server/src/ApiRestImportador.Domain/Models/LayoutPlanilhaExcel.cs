using ApiRestImportador.Domain.Commands.Importacao;
using ApiRestImportador.Domain.DTO;
using ApiRestImportador.Domain.Validations.Importacao;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace ApiRestImportador.Domain.Models
{
    // Entidade utilizada para as tratativas do arquivo enviado,
    // sendo essa apenas de transição e sendo uma entidade que persiste
    // suas informações na base de dados.
    public class LayoutPlanilhaExcel
    {
        #region Instâncias
        private List<ExcelProcessamentoLayoutDTO> _layoutDTO;
        private List<ItemImportacaoCommand> _itemImportacaos;
        private NewImportacaoCommand _newImportacaoCommand;
        private List<string> _erros;
        private DataSet _dataSet;

        #endregion

        #region Propriedades expostas
        public List<string> Erros => _erros;
        public bool LayoutValido => !_erros.Any();
        public NewImportacaoCommand NewImportacaoCommand => _newImportacaoCommand;
        #endregion

        /// <summary>
        /// Mapear arquivo da planilha para a entidade "LayoutDTO".
        /// </summary>
        /// <param name="file">Arquivo com planilha.</param>
        /// <returns>Retorna true/false para a condição da entidade mapeada.</returns>
        public bool MapearPlanilhaParaEntidade(IFormFile file)
        {
            /// Mapea os dados da planilha para DataSet.
            MapearArquivoDataSet(file);

            // Validar layout base.
            ValidarLayout(file);

            // Mapear os dados do DataSet para entidade base.            
            MapearDataSetParaEntidadeDTO();

            return LayoutValido &&
                _newImportacaoCommand != null &&
                _newImportacaoCommand.ItemImportacaos.Any();
        }

        /// <summary>
        /// Mapea os dados da planilha para o formato 
        /// DataSet e aplicando as configurações necessarias.
        /// </summary>
        /// <param name="file">Arquivo com planilha.</param>
        private void MapearArquivoDataSet(IFormFile file)
        {
            try
            {
                if (file.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    using (var stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        stream.Position = 0;
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            _dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                UseColumnDataType = false,

                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = true
                                }
                            });
                        }
                    }
                }
                else
                {
                    _erros = _erros ?? new List<string>();
                    _erros.Add("Arquivo diferente da extensão .xlsx.");
                }
            }
            catch (Exception ex)
            {
                _erros.Add("Não doi possível realizar o processamento da Planilha!");
            }
        }

        /// <summary>
        /// Valida o layout base do arquivo,
        /// sendo que verifica se está vazio, 
        /// e se possui a quantidade esperada de colunas.
        /// </summary>
        private void ValidarLayout(IFormFile file)
        {
            _erros = _erros ?? new List<string>();

            if (_dataSet != null)
            {
                if (_dataSet.Tables[0].Rows.Count == 0)
                {
                    _erros.Add("Planilha sem informações.");
                }

                if (_dataSet.Tables[0].Columns.Count != 4)
                {
                    _erros.Add("Planilha com quantidade de colunas diferente da esperada.");
                }
            }
            else
            {
                if (_erros.Count == 0)
                    _erros.Add("Ocorreu um erro no processamento da Planilha!");
            }
        }

        /// <summary>
        /// Mapea as informações do DataSet 
        /// para uma entidade base de processamento
        /// das informações oriundas da planilha.
        /// </summary>
        private void MapearDataSetParaEntidadeDTO()
        {
            _layoutDTO = new List<ExcelProcessamentoLayoutDTO>();
            _itemImportacaos = new List<ItemImportacaoCommand>();
            _newImportacaoCommand = new NewImportacaoCommand();


            if (_dataSet != null && !_erros.Any())
            {
                DataRow[] dra = new DataRow[_dataSet.Tables[0].Rows.Count];
                _dataSet.Tables[0].Rows.CopyTo(dra, 0);
                for (int iRow = 0; iRow < dra.Length; iRow++)
                {
                    for (int iColumm = 0; iColumm < dra[iRow].ItemArray.Length; iColumm++)
                    {
                        _layoutDTO.Add(new ExcelProcessamentoLayoutDTO(
                            iRow,
                            iColumm,
                            _dataSet.Tables[0].Columns[iColumm].ColumnName,
                            dra[iRow].ItemArray[iColumm].ToString(),
                            Type.GetType(dra[iRow].ItemArray[iColumm].GetType().FullName),
                            string.IsNullOrWhiteSpace(dra[iRow].ItemArray[iColumm].ToString())));
                    }
                }

                if (_layoutDTO.Any())
                {
                    var _linhas = _layoutDTO.GroupBy(g => g.Linha)
                                            .Select(c => c.FirstOrDefault())
                                            .ToList()
                                            .Select(s => s.Linha);


                    foreach (var iLinha in _linhas)
                    {
                        var itemSelecionado = _layoutDTO.Where(c => c.Linha == iLinha)
                                                        .OrderBy(o => o.Coluna)
                                                        .ToList();

                        var dataEntrega = !string.IsNullOrWhiteSpace(itemSelecionado[0].Valor) ? itemSelecionado[0].Valor : DateTime.Today.ToString();
                        var nomeProduto = !string.IsNullOrWhiteSpace(itemSelecionado[1].Valor) ? itemSelecionado[1].Valor : string.Empty;
                        var quantidade = !string.IsNullOrWhiteSpace(itemSelecionado[2].Valor) ? itemSelecionado[2].Valor : "0";
                        var valorUnitario = !string.IsNullOrWhiteSpace(itemSelecionado[3].Valor) ? itemSelecionado[3].Valor : "0";

                        _itemImportacaos
                            .Add(new ItemImportacaoCommand(iLinha + 2,
                                                           nomeProduto,
                                                           Convert.ToDateTime(dataEntrega),
                                                           Convert.ToInt32(quantidade),
                                                           Convert.ToDecimal(valorUnitario)));
                    }

                    _newImportacaoCommand.AdicionarItens(_itemImportacaos);
                }
            }
        }
    }
}
