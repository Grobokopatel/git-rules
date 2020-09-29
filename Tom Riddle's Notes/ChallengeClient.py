import requests
import json
import math
import string
import time

team_token = ''
round = 'projects-course-1'
taskType = 'starter'

url = "https://task-challenge.azurewebsites.net/api/tasks/"
print("+=========================+")
print("Client for Challenge is ON!")
print("+=========================+")

while True:
	print("press Enter to get task")
	input()
	print("I`ll get task after 2 sec")
	time.sleep(1)

	response = requests.post(url, params = {'secret': team_token, 'round': round, 'type': taskType})
	json_response = response.json()
	print("You hava a new task:")
	print(f"TaskId:		{json_response['id']}")
	print(f"TaskType:		{json_response['typeId']}")
	print(f"Question:		{json_response['question']}")
	task_type = json_response['typeId']
	question = json_response['question']
	task_id = json_response['id']


	value = 42


	print(f"Answer:		{value}")
	print(f"Press Enter to send answer!")
	input()
	print("I`m sending your answer to server!")
	print("==================================================================\n\n")
	answer = json.dumps({"answer": str(value)})
	header = {'Content-Type':'application/json'}

	response = requests.post(url + task_id, headers = header, params = {'secret': team_token}, data = answer)
	json_response = response.json()
	if json_response['status'] == 1:
		print(f"Good work status was {json_response['status']}")
		print("==================================================================\n\n")
	else:
		print(f"Err0r")
		break
