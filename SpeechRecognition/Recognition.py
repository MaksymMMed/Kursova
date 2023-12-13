import Functions as fnc
from flask import Flask, request
from flask_cors import CORS
import os

app = Flask(__name__)
CORS(app, resources={r"/Recognize": {"origins": "http://localhost:3000"}})


@app.route('/Recognize', methods=['POST'])
def predict():     
        file_audio_test = request.files['file']
        audio_directory = 'C:/Audio/'

        if not os.path.exists(audio_directory):
            os.makedirs(audio_directory)

        audio_path_test = os.path.join(audio_directory, 'file_test.mp3')
        file_audio_test.save(audio_path_test)

        file_path = request.form['file_path']

        print(file_path)

        y1,y2,sr = fnc.get_files(file_path,audio_path_test,44000)
    
        similarity = fnc.compute_similarity(y1,y2,sr)
    
        return str(similarity)

if __name__ == '__main__':
    app.run(debug=True)
 

