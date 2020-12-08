
# coding: utf-8

# In[39]:


from selenium import webdriver
from selenium.webdriver.common.keys import Keys
import time
import urllib.request

driver=webdriver.Chrome()
driver.get("https://www.google.co.kr/imghp?hl=ko&tab=wi&ogbl")
elem = driver.find_element_by_name("q")
elem.send_keys("식탁")
elem.send_keys(Keys.RETURN)

images = driver.find_elements_by_css_selector(".rg_i.Q4LuWd")
count = 1
for image in images:
    try:
        image.click()
        time.sleep(2)
        imgUrl=driver.find_element_by_xpath('/html/body/div[2]/c-wiz/div[3]/div[2]/div[3]/div/div/div[3]/div[2]/c-wiz/div[1]/div[1]/div/div[2]/a/img').get_attribute("src")
        urllib.request.urlretrieve(imgUrl, "식탁"+str(count) + ".jpg")
        count=count+1
    except:
        pass
    
driver.close()

