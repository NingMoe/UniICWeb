-- Contribution by Gustavo Lyrio
require"imlua"
require"imlua_process"
require"cdlua"
require"cdluaim"
require"iuplua"
require"iupluacd"

for i = 1,21,1 do
	image1 = im.FileImageLoad("icon\\"..i..".png");
	image2 = im.ProcessCropNew(image1,55,450,77,450)
	image3 = im.ImageCreateBased(image2, 50, 50)
	im.ProcessReduce(image2, image3, 0)
	image3:Save("icon_s\\"..i..".png", "PNG")
end
