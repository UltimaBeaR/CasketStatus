using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CasketStatusMobileDeviceEmulator
{
    public partial class FormEmulator : Form
    {
        public FormEmulator()
        {
            InitializeComponent();
        }

        private async void buttonGetResponse_Click(object sender, EventArgs e)
        {
            var buttonText = buttonGetResponse.Text;

            try
            {
                buttonGetResponse.Text = "В процессе...";
                buttonGetResponse.Enabled = textBoxHostName.Enabled = textBoxResponse.Enabled = checkBoxNeedDescription.Enabled = false;

                textBoxResponse.Text = await GetCasketCurrentStatusAsync(textBoxHostName.Text, checkBoxNeedDescription.Checked);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Что то пошло не так\n\nТип ошибки: {ex.GetType().FullName}\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buttonGetResponse.Text = buttonText;
                buttonGetResponse.Enabled = textBoxHostName.Enabled = textBoxResponse.Enabled = checkBoxNeedDescription.Enabled = true;
            }
        }

        // Получает ответ на api запрос от Ларец.Статус на получение текущего статуса в виде json строки, либо падает с Exception-ом
        private async Task<string> GetCasketCurrentStatusAsync(string url, bool needDescription)
        {
            // Создаем WebClient и выполняем через него удаленный restful метод

            WebClient webClient = new WebClient();
            // Указываем encoding, иначе тупит
            webClient.Encoding = Encoding.UTF8;

            var urlLeftPart = (new Uri(url)).GetLeftPart(UriPartial.Authority);

            if (urlLeftPart == string.Empty)
                throw new UriFormatException("Некорректный URL");

            string restfulAPIUrl = urlLeftPart + "/api/status" + (needDescription ? "?description=true" : "");

            // Собственно, запрос
            return await webClient.DownloadStringTaskAsync(restfulAPIUrl);
        }
    }
}
