

function  SaveByte( path,msg)

--print(path..msg)
file = io.open( path, "a")

-- 在文件最后一行添加 Lua 注释
file:write(msg)

-- 关闭打开的文件
file:close()



end


--msg
SaveByte("D:\\WorkAndStudy/Lua/outPut.txt","lua++++++")
