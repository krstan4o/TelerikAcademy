﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _11.Groups.Data;
using System.Transactions;

class Program
{
    public static void InsertUser(string userName)
    {
        using (TransactionScope scope = new TransactionScope())
        {
            using (GroupsEntities context = new GroupsEntities())
            {
                Group adminGroup = new Group { GroupName = "Admins" };
                if (context.Groups.Count(x => x.GroupName == "Admins") == 0)
                {
                    context.Groups.Add(adminGroup);
                    context.SaveChanges();
                    scope.Complete();
                }
                else
                {
                    if (context.Users.Count(x => x.UserName == userName) > 0)
                    {
                        Console.WriteLine("User already exists.");
                        scope.Dispose();
                    }

                    Group currentgroup = context.Groups
                        .Where(x => x.GroupName == "Admins").First();

                    User newUser = new User()
                    {
                        UserName = userName,
                        GroupID = currentgroup.GroupID
                    };

                    context.Users.Add(newUser);
                    context.SaveChanges();
                    scope.Complete();
                }
            }
        }

    }
    static void Main(string[] args)
    {
        InsertUser("Kolio madoto");
    }
}