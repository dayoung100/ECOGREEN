#구글 웹크롤링
from urllib.request import urlretrieve
from urllib.parse import quote_plus
from bs4 import BeautifulSoup
from selenium import webdriver

print("Hello")
search = input('검색어: ')
url=f'https://www.google.com/search?q={quote_plus(search)}&source=lnms&tbm=isch&sa=X&ved=2ahUKEwj6-onw9f7sAhXsyYsBHVmRDGIQ_AUoAXoECBgQAw'

#괄호 안에 chromedriver의 절대경로
#chromedriver 다운로드는 https://hello-bryan.tistory.com/193 의 설명 참고
driver = webdriver.Chrome("D:\ChromeDriver\chromedriver.exe") 
driver.get(url)

for i in range(500): #최대 500개
    driver.execute_script("window.scrollBy(0,10000)")

html=driver.page_source
soup=BeautifulSoup(html)

img=soup.select('.rg_i.Q4LuWd')
n=1
imgurl=[]

for i in img:
    try:
        imgurl.append(i.attrs["src"])
    except KeyError:
        imgurl.append(i.attrs["data-src"])

for i in imgurl:
    urlretrieve(i, "크롤링사진/"+search+str(n)+".jpg") #크롤링사진이라는 폴더 미리 생성해두어야
    n += 1
    print(imgurl)
    if(n==120): #120개로 제한
        break

driver.close()