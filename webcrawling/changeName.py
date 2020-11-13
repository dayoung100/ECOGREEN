import os

file_path='C:/Users/user/Desktop/crawling/크롤링사진'
file_names = os.listdir(file_path)

i=1
for name in file_names:
    src=os.path.join(file_path, name)
    dst = 'wardrobe_' + str(i) + '.jpg' // rename file
    dst = os.path.join(file_path, dst)
    os. rename(src, dst)
    i+=1
