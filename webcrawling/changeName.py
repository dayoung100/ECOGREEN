import os

file_path='C:/Users/user/Desktop/crawling/크롤링사진'
file_names = os.listdir(file_path)

i=1
for name in file_names:
    src=os.path.join(file_path, name)
    dst = 'wardrobe_' + str(i) + '.jpg' // 파일 이름 변경하고 싶을 경우 이 부분 변경
    dst = os.path.join(file_path, dst)
    os. rename(src, dst)
    i+=1
