using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SecondTestTaskB1.Interfaces;
using SecondTestTaskB1.Models;

namespace SecondTestTaskB1.Services
{
    /// <summary>
    /// Сервис для обработки Excel-файлов и преобразования их данных в список аккаунтов.
    /// </summary>
    public class ExcelService : IExcelService
    {
        public async Task<List<Account>> ProcessExcelFile(IFormFile file, int fileId)
        {
            var accounts = new List<Account>();

            // Получаем расширение файла и проверяем, поддерживается ли формат
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (extension != ".xls" && extension != ".xlsx")
                throw new Exception("Формат файла не поддерживается.");

            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0; // Сбрасываем позицию потока на начало

                    // Определяем тип книги в зависимости от расширения файла
                    IWorkbook workbook = extension switch
                    {
                        ".xls" => new HSSFWorkbook(stream),
                        ".xlsx" => new XSSFWorkbook(stream),
                        _ => throw new Exception("Формат файла не поддерживается.")
                    };

                    // Получаем первый рабочий лист из книги
                    var sheet = workbook.GetSheetAt(0);
                    if (sheet == null)
                        throw new Exception("Не найден рабочий лист в файле."); // Ошибка, если лист не найден

                    int startRowIndex = 8;
                    string currentClass = string.Empty;
                    List<Account> currentClassAccounts = new List<Account>();
                    bool isClassData = false; // Флаг, указывающий, что данные принадлежат классу

                    // Перебираем все строки на листе начиная с указанного индекса
                    for (int rowIndex = startRowIndex; rowIndex <= sheet.LastRowNum; rowIndex++)
                    {
                        var row = sheet.GetRow(rowIndex);
                        if (row == null) continue; // Пропускаем пустые строки

                        // Проверяем, содержит ли первая ячейка информацию о классе
                        var classHeader = GetStringCellValue(row, 0); // Получаем значение из первой ячейки
                        if (!string.IsNullOrEmpty(classHeader) && classHeader.StartsWith("КЛАСС"))
                        {
                            // Если нашли класс, сохраняем текущие аккаунты и начинаем новый набор данных
                            if (currentClassAccounts.Any())
                            {
                                accounts.AddRange(currentClassAccounts); // Добавляем текущие аккаунты в общий список
                            }

                            // Обновляем информацию о текущем классе
                            currentClass = classHeader;
                            currentClassAccounts = new List<Account>(); // Очищаем список для нового класса
                            isClassData = true; // Включаем флаг для обработки данных класса
                            continue; // Переходим к следующей строке
                        }

                        if (isClassData)
                        {
                            var accountNumber = GetStringCellValue(row, 0);
                            var openingActive = GetDecimalCellValue(row, 1);
                            var openingPassive = GetDecimalCellValue(row, 2);
                            var debit = GetDecimalCellValue(row, 3);
                            var credit = GetDecimalCellValue(row, 4);
                            var closingActive = GetDecimalCellValue(row, 5);
                            var closingPassive = GetDecimalCellValue(row, 6);

                            var account = new Account
                            {
                                AccountNumber = accountNumber,
                                OpeningActive = openingActive,
                                OpeningPassive = openingPassive,
                                Debit = debit,
                                Credit = credit,
                                ClosingActive = closingActive,
                                ClosingPassive = closingPassive,
                                AccountClass = currentClass,
                                FileId = fileId
                            };

                            currentClassAccounts.Add(account);
                        }
                    }

                    // Добавляем оставшиеся аккаунты после завершения обработки всех строк
                    if (currentClassAccounts.Any())
                    {
                        accounts.AddRange(currentClassAccounts);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при обработке файла: {ex.Message}", ex);
            }

            return accounts;
        }
        /// <summary>
        /// Получает строковое значение из ячейки Excel.
        /// </summary>
        /// <param name="row">Строка Excel.</param>
        /// <param name="cellIndex">Индекс ячейки.</param>
        /// <returns>Значение ячейки как строка.</returns>
        public string GetStringCellValue(IRow row, int cellIndex)
        {
            var cell = row.GetCell(cellIndex);
            return cell != null ? cell.ToString().Trim() : string.Empty;
        }
        /// <summary>
        /// Получает числовое значение из ячейки Excel.
        /// </summary>
        /// <param name="row">Строка Excel.</param>
        /// <param name="cellIndex">Индекс ячейки.</param>
        /// <returns>Числовое значение ячейки.</returns>
        public decimal GetDecimalCellValue(IRow row, int cellIndex)
        {
            var cell = row.GetCell(cellIndex);
            return cell != null && decimal.TryParse(cell.ToString(), out var result) ? result : 0;
        }
    }
}