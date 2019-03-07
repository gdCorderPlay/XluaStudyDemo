using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDLua
{
    /// <summary>
    /// Lua文本
    /// </summary>
    public class LuaScripts
    {
        public const string Lua_IntCapture = @"
       a,b=1,2
       print(a+b)
      ";
        public const string Lua_BoolCapture = @"
       a,b=true,false
       if b then
        print('b=true')
       else 
          print('b=false')

        end
         print(a,'true')
         print(a,b,a~=b)  --  ~= 不等于
      ";
        public const string Lua_StringCapture = @"
         str='ab' 
          print(str,#str)--# 操作符获取字符串的长度
        ";

        public const string Lua_TableCapture = @"
          a='abc'
          b='bcd'
          c=function()
             print('function c')
           return 'c'
           end       
         table={-- lua中的表的第一个元素下标为1
          a,b,c
          }
          print('table[1]=',table[1])-- 正确的获取表中元素的方法
          print('table[2]=',table[2]) 
          print('table[3]=',table[3])
          print('table[3]=',table[3]())


          print('table.a=',table.a)  --错误的获取表中元素的方法
          print('table.b=',table.b)
          print('table.c=',table.c)
          --print('table.c=',table.c())

          print('table.length',#table) --表的长度
         
           for i=1,3,1  do

           print(table[i])
           end
         ";
        public const string Lua_WhileCapture = @"
            a=10
            while a>0 do
              if a%2==0 then
              print('偶数',a)   
              else
              print('齐数',a)   
              end
              a=a-1
            end 

          ";
        public const string Lua_RepeatCapture = @"
            a=10
            repeat  -- 执行的语法块
              if a%2==0 then
              print('偶数',a)   
              else
              print('齐数',a)   
              end
              a=a-1
            until a<=0 -- until 的条件满足时跳出

          ";
        public const string Lua_ForCapture = @"
           
            for   a=1,10,1 do
              if a%2==0 then
              print('偶数',a)   
              else
              print('齐数',a)   
              end
          end

          ";
        public const string Lua_IpairsCapture = @"
           
           funcs={
              [2]= function()
                 return 'aaaaa'
                 end ,
              [1]= function()
                 return 'bbbbb'
                 end ,
              [5]= function()
                 return 'cccc'
                 end ,
               [3]=function()
                 return 'dddd'
                 end 
                }

            for k,v in ipairs(funcs) do --从下标1开始 到断点的下标
                  print(k, v())
          end

          ";
        public const string Lua_PairsCapture = @"
           
           funcs={
              [2]= function()
                 return 'aaaaa'
                 end ,
              [1]= function()
                 return 'bbbbb'
                 end ,
              [5]= function()
                 return 'cccc'
                 end ,
               [3]=function()
                 return 'dddd'
                 end 
                }

            for k,v in pairs(funcs) do --遍历所有的元素 顺序不定
                  print(k, v())
          end

          ";
        public const string Lua_FunctionCapture = @"
                     add=function(num1, num2)
                          print(num1+num2)
                          end
                     a ={
                       ['adddd']= add,
                          'c'

                            }
                     print(a.adddd(1,2))
                       ";

        public const string Lua_SumFunction = @"
         sum=function(a)
          su=0;
          for i,v in ipairs(a) do
          su=su+v
           end
          return su
         end
         
table={1,2,3,4,5,6,7,8,9,10}
print(sum(table))  

";
        public const string Lua_OrFunction = @"
         sum=function(a)
          a=a or 1
          return a;
         end
         

       print(sum())   --1
  print(sum(2))   --2 
 print(sum(0))   --0
";

        public const string Lua_UnpackFunction = @"
        tab={'e','f','g'}
       a,b,c=unpack{'a','b','c'}
        print(a,b,c)
         a,b,c=unpack(tab)
          print(a,b,c)
";
        public const string Lua_PramsFunction = @"
      sum= function(...)
       su=0;
for i,k in ipairs({...}) do
su=su+k

end
return su
end
print(sum(1,2,3,4,5,6,7,8,9,10))
";
        public const string Lua_SelectFunction = @"
             
        log=function(...)
         for i=1,select('#',...) do
           print(i,select(i,...))
         end
end
        log(1,2,3,4,5,6,7,8,nil,9,10)

";
        public const string Lua_SortFunction = @"
           nums={10,12,3,4,5,6,7}
           print('排序前') 
           for i,k in ipairs(nums) do
             print(i,k)
           end
     
           table.sort(nums,function(a,b)return (a<b)end ) -- 升序排列（a>b） 降序排列
             print('排序后') 
           for i,k in ipairs(nums) do
           print(i,k)
           end
";
        public const string Lua_RemoveFunction = @"
           nums={10,12,3,4,5,6,7}
              
          remove=function(a,cc)
          b={}
          for i,v in ipairs(a) do
           
               if v~=cc then
               table.insert(b,v)
               end
          end
           a=b
           return a
         end 
       
          nums= remove(nums,5)

           for i,k in ipairs(nums) do
             print(i,k)
           end
     
         
";
        /// <summary>
        /// 计数器
        /// </summary>
        public const string Lua_CountFunction = @"
          newCal=function()
         local  i=0;
          return function()
              i=i+1
             return i
        end
end

c1=newCal()

print( 1,c1())
print( 1,c1())
print( 1,c1())

c2=newCal()
print( 2,c2())
print( 4,c1())

         
";
        /// <summary>
        /// 迭代器 测试
        /// </summary>
        public const string Lua_IterateFunction = @"

         values=function(t)
          local  k=0
          return function() 
                  k=k+1  
                  return t[k]
                  end 
               end 
        nums={12,11,10}
for v in values(nums) do

print(v)
end

       ";


        public static List<string> scripts = new List<string>() {
            Lua_IntCapture,//int 0
            Lua_BoolCapture,//bool 1
            Lua_StringCapture,//string 2
            Lua_TableCapture,//table 3
            Lua_WhileCapture,//控制结构 while 4
            Lua_RepeatCapture,// repeat- until 5
            Lua_ForCapture, //for 6
            Lua_IpairsCapture,//迭代器 7 ipairs 数组遍历器 遇到nil会退出  只会遍历数组下标的key 从1开始key的升序遍历
            Lua_PairsCapture,// 8          pairs 表遍历器  遇到nil不会退出 遍历所有的元素  
            Lua_FunctionCapture,//函数 9
            Lua_SumFunction,//累加函数10
            Lua_OrFunction,//or 操作符 11
            Lua_UnpackFunction, // unpack 12 按下标返回数组元素
            Lua_PramsFunction,// ... 可变参数类型 13
            Lua_SelectFunction,// select 可变参数类型的安全获取 14
            Lua_SortFunction,//表的排序操作 15
            Lua_RemoveFunction,//删除表中的指定元素 16
            Lua_CountFunction,//计数器 17
            Lua_IterateFunction,//迭代器 18
        };



    }

}

