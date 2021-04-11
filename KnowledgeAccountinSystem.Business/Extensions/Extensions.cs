using KnowledgeAccountinSystem.Business.Models;
using KnowledgeAccountinSystem.Data;
using KnowledgeAccountinSystem.Data.Entities;
using System;
using System.Linq;

namespace KnowledgeAccountinSystem.Business.Extensions
{
    public static class Extensions
    {
        public static bool IsModelValid(this UserModel model)
        {
            if (string.IsNullOrEmpty(model.Name) ||
               string.IsNullOrEmpty(model.Surname) ||
               string.IsNullOrEmpty(model.Email) ||
               string.IsNullOrEmpty(model.Password) ||
               !char.IsUpper(model.Name[0]) ||
               !char.IsUpper(model.Surname[0]))
                return false;
            else
                return true;
        }

        public static bool IsAccountNotExist(this int id, IUnitOfWork context)
        {
            if (context.AccountRepository.GetAll().Select(x => x.Id).Contains(id))
                return false;
            else
                return true;
        }

        public static bool IsSkillModelNotValid(this SkillModel model)
        {
            if (Enum.GetValues(typeof(SkillName)).Cast<SkillName>().Contains(model.Name) ||
                Enum.GetValues(typeof(SkillLevel)).Cast<SkillLevel>().Contains(model.Level))
                return false;
            else 
                return true;
        }

        public static bool IsSkillExist(this SkillModel model, IUnitOfWork context, int id)
        {
            if (context.SkillRepository.GetAllByProgrammerId(id).Select(x => x.Name).Contains(model.Name))
                return true;
            else
                return false;
        }

        public static bool IsEmailNotExist(this string email, IUnitOfWork context)
        {
            if (context.AccountRepository.GetAll().Select(x => x.Email).Contains(email))
                return false;
            else
                return true;
        }
    }
}
