using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using HRMapp.Attendents;
using HRMapp.Contacts;
using HRMapp.Contracts;
using HRMapp.Departments;
using HRMapp.Employees;
using HRMapp.Salarys;
using HRMapp.Shifts;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace HRMapp.Contacts;

public class ContactDataSeeder
    : IDataSeedContributor, ITransientDependency
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IContactRepository _contactRepository;
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;

    

    public ContactDataSeeder(
        IGuidGenerator guidGenerator,
        ICurrentTenant currentTenant,
        IContactRepository contactRepository,
        IEmployeeRepository employeeRepository
        
    )
    {
        _employeeRepository = employeeRepository;
        _contactRepository = contactRepository;
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await AddContactSeeder(context);
    }
    
        public async Task AddContactSeeder(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            if (await _contactRepository.GetCountAsync() > 0)
            {
                return;
            }
            var tenantId = context?.TenantId;
            List<string> ten_viet_nam = new List<string>
            {
                "Đỗ Đức Hùng","Nguyễn Bình", "Trần Vũ Hoàn", "Nguyễn Văn Lĩnh", "Nguyễn Minh Tuấn", "Trần Quốc Vương",
                "Nguyễn Thanh Hương", "Lê Văn Duy", "Phạm Thị Lan", "Hoàng Minh Tuấn", "Vũ Thị Mai",
                "Nguyễn Đình Quyết", "Vũ Cao Lâm", "Đặng Văn Quang", "Trần Thị Diễm", "Nguyễn Hải Dương",
                "Trần Thanh Tâm", "Lê Thị Thu", "Phạm Văn Hùng", "Hoàng Đức Huy", "Vũ Thị Thu Hà",
                "Nguyễn Thị Như Quỳnh", "Lê Hoàng Quân", "Đinh Văn Phượng", "Lê Văn Minh Châu", "Nguyễn Xuân Sang",
                "Lê Phú Quý", "Lý Quốc Quyền", "Bùi Minh Quân", "Nguyễn Ngọc Sơn", "Bùi Duy Qúy",
                "Nguyễn Vũ Ngọc Quyên", "Trương Trọng Quân", "Phạm Minh Quân", "Võ Thanh Sanh", "Nguyễn Minh Phương",
                "Nguyễn Viết Sơn", "Vũ Hoàng Hải Sơn", "Nguyễn Hải Sơn", "Nguyễn Vũ Trường Sơn", "Trần Minh Sang",
                "Nguyễn Trung Sơn", "Lê Quang", "Vũ Thị Phương", "Nguyễn Văn Sơn", "Lê Anh Sơn",
                "Võ Tiến Sĩ", "Phan Đức Sơn", "Trần Thế Sơn", "Vũ Hồng Tâm", "Nguyễn Minh Tân",
                "Lê Hà Xuân Thái", "Nguyễn Ngọc Phương", "Bùi Anh Tài", "Dương Chí Tâm", "Phạm Thanh Quốc"
            };
            List<Contact> contacts =new List<Contact>();
            
            foreach (string name in ten_viet_nam)
            {
                Random random = new Random();

           
                DateTime startDate = new DateTime(1993, 1, 1);
                DateTime endDate = new DateTime(2001, 12, 31);
                int range = (endDate - startDate).Days;

                DateTime randomDate = startDate.AddDays(random.Next(range));
                string email = $"{RemoveDiacritics(name.Replace(" ","")).ToLower()}@abp.io.vn"; // Generate email based on name
                string phone = "0933146147"; // Sample phone number

                Contact contact = new Contact(_guidGenerator.Create(), tenantId, name, Gender.Male, randomDate, true, email, phone, "Việt Nam");
                contacts.Add(contact);
            }
            await _contactRepository.InsertManyAsync(contacts);
        }
        
        
        
    }

        // Hàm chuyển đổi tên có dấu thành tên không dấu
        static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            string formD = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char ch in formD)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(ch);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
}