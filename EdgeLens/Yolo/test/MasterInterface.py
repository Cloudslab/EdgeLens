import os
import time

while True:
	if(os.path.exists("./images/input.jpg")):
		os.system("python3 test_images.py params.py")
		os.system("rm ./images/input.jpg")
	else:
		time.sleep(0.1)