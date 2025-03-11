using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

public class OrangeHRMTest
{
    public static void RunScript()
    {
        // Khởi tạo WebDriver
        IWebDriver driver = new ChromeDriver();
        driver.Manage().Window.Maximize();

        try
        {
            // Bước 1: Truy cập trang đăng nhập
            driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");
            Thread.Sleep(6000); // Chờ load trang

            // Đăng nhập
            driver.FindElement(By.Name("username")).SendKeys("Admin");
            driver.FindElement(By.Name("password")).SendKeys("admin123");
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(8000); // Chờ đăng nhập thành công

            // Bước 2: Truy cập menu PIM -> Employee List
            driver.FindElement(By.XPath("//span[text()='PIM']")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//a[text()='Employee List']")).Click();
            Thread.Sleep(8000);

            // Bước 3.1: Thêm 1 nhân viên mới
            driver.FindElement(By.XPath("//button[text()=' Add ']")).Click();
            Thread.Sleep(8000);

            //// Tạo Employee ID ngẫu nhiên
            Random rand = new Random();
            int employeeId = rand.Next(100000, 999999);

            // Nhập thông tin nhân viên
            driver.FindElement(By.Name("firstName")).SendKeys("Phan");
            driver.FindElement(By.Name("lastName")).SendKeys("Phuong");

            // Tìm ô nhập Employee ID (có thể cần cập nhật XPath nếu khác)
            //IWebElement empIdField = driver.FindElement(By.XPath("//label[text()='Employee Id']/following-sibling::div//input"));
            //empIdField.Clear();  // Xóa giá trị mặc định nếu có
            //empIdField.SendKeys(employeeId.ToString());

            // Lưu nhân viên
            driver.FindElement(By.XPath("//button[text()=' Save ']")).Click();
            Thread.Sleep(8000);

            Console.WriteLine("Đã thêm nhân viên mới thành công!");

            // Bước 3.2: Tìm kiếm nhân viên theo Job Title
            driver.FindElement(By.XPath("//a[text()='Employee List']")).Click();
            Thread.Sleep(8000);

            // Mở bộ lọc tìm kiếm
            driver.FindElement(By.XPath("//i[@class='oxd-icon bi-filter oxd-button-icon']")).Click();
            Thread.Sleep(8000);

            // Chọn Job Title từ dropdown
            IWebElement jobTitleDropdown = driver.FindElement(By.XPath("//label[text()='Job Title']/following::div[1]"));
            jobTitleDropdown.Click();
            Thread.Sleep(8000);
            driver.FindElement(By.XPath("//div[@role='option']//span[text()='Software Engineer']")).Click();
            Thread.Sleep(8000);

            // Nhấn nút tìm kiếm
            driver.FindElement(By.XPath("//button[text()=' Search ']")).Click();
            Thread.Sleep(8000);

            Console.WriteLine("Tìm kiếm nhân viên theo Job Title thành công!");
            Thread.Sleep(8000);


        }
        catch (Exception ex)
        {
            Console.WriteLine("Có lỗi xảy ra: " + ex.Message);
        }
        finally
        {
            // Đóng trình duyệt
            driver.Quit();
        }
    }
}
