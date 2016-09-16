using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;
using System.Data.Entity.Infrastructure;

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Text;

namespace MvcApplication2.Models
{
    public class UsersContext2 : DbContext
    {
        public UsersContext2()
            : base("DefaultConnection1")
        {
            //Database.SetInitializer(new  MigrateDatabaseToLatestVersion<UsersContext2, MvcApplication2.Migrations.Configuration>("DefaultConnection1"));
        }

        private Exception HandleDbUpdateException(DbUpdateException dbu)
        {
            var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");

            try
            {
                foreach (var result in dbu.Entries)
                {
                    builder.AppendFormat("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
                }
            }
            catch (Exception e)
            {
                builder.Append("Error parsing DbUpdateException: " + e.ToString());
            }

            string message = builder.ToString();
            return new Exception(message, dbu);
        }
        public override int SaveChanges()
    {
        try
        {
            return base.SaveChanges();
        }
        catch (DbEntityValidationException e)
        {
         
        
            foreach (var eve in e.EntityValidationErrors)
            {
                Debug.WriteLine(@"Entity of type \{0}\ in state \{1}\ has the                                  following validation errors:",                     eve.Entry.Entity.GetType().Name, eve.Entry.State);
                foreach (var ve in eve.ValidationErrors)
                {
                    Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                        ve.PropertyName, ve.ErrorMessage);
                }
            }
            throw;
        }
        catch(DbUpdateException e)
        {
           var exception = HandleDbUpdateException(e);
            throw exception;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            throw;
        }
        return 1;
    }

        public DbSet<Emp> Emps { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<AccountMaster> Accounts { get; set; }
        public DbSet<GroupMaster> GroupMasters { get; set; }
        public DbSet<TransMain> TransMains {get;set;}
        
        public DbSet<Transcation> Transcations { get; set; }
        public DbSet<TransDetails> TransDetails { get; set; }
        public DbSet<ItemMaster> ItemMasters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure domain classes using Fluent API here

            base.OnModelCreating(modelBuilder);
        }

        public static void SetProperties<TIn, TOut>(TIn input, TOut output, ICollection<string> includedProperties)
            where TIn : class
            where TOut : class
        {
            if ((input == null) || (output == null)) return;
            Type inType = input.GetType();
            Type outType = output.GetType();
            foreach (PropertyInfo info in inType.GetProperties())
            {
                PropertyInfo outfo = ((info != null) && info.CanRead)
                    ? outType.GetProperty(info.Name, info.PropertyType)
                    : null;
                if (outfo != null && outfo.CanWrite
                    && (outfo.PropertyType.Equals(info.PropertyType)))
                {
                    if ((includedProperties != null) && includedProperties.Contains(info.Name))
                        outfo.SetValue(output, info.GetValue(input, null), null);
                    else if (includedProperties == null)
                        outfo.SetValue(output, info.GetValue(input, null), null);
                }
            }
        }


      //  public void DetailUpdate<MasterClass, DetailsClass>(MasterClass emp,string DetailsClassProperyName)  
      //      where MasterClass : class
      //     where DetailsClass: class
      //  {
      //      DbSet<DetailsClass> temp =  (DbSet < DetailsClass > )this.getValue(this, "PhoneData123"); 
            
      //      //ObjectContext objectContext = ((IObjectContextAdapter)this).ObjectContext;
      //      //ObjectSet<MasterClass> set = objectContext.CreateObjectSet<MasterClass>();


      //      //IEnumerable<string> keyNames = set.EntitySet.ElementType
      //      //                                            .KeyMembers
                                            
      //      //                                            .Select(k => k.Name);
           
            
      //      // if(keyNames.Count()>0     )
      //      //{ 
      //      //string MasterKeyFieldName = keyNames.First().ToString();
      //      //    PropertyInfo MasterPorperty=inType.GetProperty(MasterKeyFieldName,typeof(int) ) ;
      //      //    int MasterKeyId=(int)MasterPorperty.GetValue(emp, null);
               

      //      //}
      //      Type inType = emp.GetType();
      //       IEnumerable<Phone> tempSub = null;
      //       tempSub = this.Phones.Where(p => p.EmpId == (int)getKeyValue(emp) );

      //       PropertyInfo PhoneProperty = inType.GetProperty(DetailsClassProperyName);
      //       ICollection<Phone> Phones = (ICollection<Phone>)PhoneProperty.GetValue(emp, null);



      //       foreach (Phone ss in tempSub)
         
      //       {


      //           var test = Phones.Where(p => p.PhoneId == (int)getKeyValue(ss));
      //           if (test.Count() == 0)
      //           {
      //               this.Phones.Remove(ss);
      //           }
      //       }

      //       foreach (Phone newPhone in Phones)
      //      {


      //          if (newPhone.PhoneId == 0)
      //          {
      //              this.Entry(newPhone).State = EntityState.Added;
      //              this.Phones.Add(newPhone);

      //          }
      //          else
      //          {
      //              var oldPhone = this.Phones.Find((int)getKeyValue(newPhone));
      //              var attachedEntry = this.Entry(oldPhone);

      //              attachedEntry.CurrentValues.SetValues(newPhone);



      //          }
          
          
      //      }
      //Phones.Clear(); 
         
      //      //emp.phones.Clear();
      //  }


      //  //public IEnumerable<DbSet<T>> GetDbSetsByType<T>() where T : class
      //  //{
      //  //    //var flags = BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance;
      //  //    var props = GetType().GetProperties()
      //  //        .Where(p => p.PropertyType.IsGenericType && p.PropertyType.Name.StartsWith("DbSet"))
      //  //        .Where(p => p.PropertyType.GetGenericArguments().All(t => t == typeof(T)));
      //  //    return props.Select(p => (DbSet<T>)p.GetValue(this, null));
      //  //}
      //  //public object getValue<MasterClass>(MasterClass obj,string Name)
      //  //       where MasterClass : class
      //  //{
      //  //    object value=null;
      //  //    Type type = obj.GetType();
            
            
            
      //  //        PropertyInfo MasterPorperty = type.GetProperty(Name);
      //  //        value = MasterPorperty.GetValue(obj, null);
                

            


      //  //    return value;

      //  //}
      //  //public object getKeyValue<MasterClass>(MasterClass obj)
      //  //       where MasterClass : class
      //  //{
      //  //    object value=null;
      //  //    Type type = obj.GetType();
      //  //    ObjectContext objectContext = ((IObjectContextAdapter)this).ObjectContext;
      //  //    ObjectSet<MasterClass> set = objectContext.CreateObjectSet  <MasterClass>();
      //  //    IEnumerable<string> keyNames = set.EntitySet.ElementType
      //  //                                              .KeyMembers
      //  //                                              .Select(k => k.Name);

            
      //  //    if (keyNames.Count() > 0)
      //  //    {
      //  //        string MasterKeyFieldName = keyNames.First().ToString();
      //  //        PropertyInfo MasterPorperty = type.GetProperty(MasterKeyFieldName, typeof(int));
      //  //        value = (int)MasterPorperty.GetValue(obj, null);
                

      //  //    }


      //  //    return value;

      //  //}

    }

    [Table("ItemMaster")]
    public class ItemMaster
    {
        public int ItemMasterId { get; set; }
        public string ItemName { get; set; }
        public decimal SalesRate { get; set; }


    }

    [Table("Emp")]
    public class EmpBase
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int EmpId { get; set; }

        [Required]
        public string EmpName { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string ImagePath { get; set; }
        

    }
    [Table("AccountMaster")]
    public class AccountMaster
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AccountMasterId { get; set; }
        public string AccountName { get; set; }
        public int GroupMasterId { get; set; }
        public virtual GroupMaster GroupMaster { get; set; }
       // public virtual ICollection<Transcation> Transcation { get; set; }

    }

    [Table("GroupMaster")]
    public class GroupMaster
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int GroupMasterId { get; set; }
        public string GroupName { get; set; }
    }

  
    [Table("TransMain")]
    [Serializable]
    public class TransMain
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TransMainId { get; set; }
        public string BillNo { get; set; }
        public DateTime  BillDate { get; set; }
        public string Remarks { get; set; }
        public int TypeMasterId { get; set; }

        public virtual ICollection<TransDetails> TransDetails { get; set; }    
       public virtual ICollection <Transcation> Transcation { get; set; }

    }

    [Table("Transcation")]
    [Serializable]
    public class Transcation
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        
        public int TranscationId { get; set; }
        public int TransMainId { get; set; }
        public int Srno { get; set; }
        public int AccountMasterId { get; set; }
        public virtual AccountMaster AccountMaster { get; set; }
        public int CrDrMasterId { get; set; }
        public decimal Amount { get; set; }
        public virtual ICollection<TransDetails> TransDetails { get; set; }

    }
    [Table("TransDetails")]
    [Serializable]
    public class TransDetails
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TransDetailsId { get; set; }
        public int TranscationId { get; set; }
        public int TransMainId { get; set; }
        public int Srno { get; set; }
        public int ItemMasterId { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }

        public virtual ItemMaster ItemMaster { get; set; }
    }
    [Table("Emp")]
    [Serializable]
    public class Emp 
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int EmpId { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(4)]
        [Display(Name = "Emp Name :")]
        public string EmpName { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Amount :")]
        public decimal Amount { get; set; }

        
        public string ImagePath { get; set; }
        public string Phone { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Please enter correct email address")]
            public string Email { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string Pass { get; set; }

        public string City { get; set; }


        public virtual ICollection<Phone> phones { get; set; }
        
    }

    [Table("Image")]
    public class Image
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int  ImageId { get; set; }

        [Required]
        public string ImagePath { get; set; }

    }

    [Table("PhoneData")]
    [Serializable]
    public class Phone
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PhoneId { get; set; }
        public   int EmpId { get; set; }
        public virtual  Emp Emp { get; set; }

        
        public string PhoneName { get; set; }

        
        
        public string PhoneNumber { get; set; }
    }
}


