import librosa				
import numpy as np
import librosa.display						
from scipy.signal import filtfilt

#Нормалізація
def smooth_mfcc(mfcc, window_len=2):		
    window = np.ones(window_len)/float(window_len)									
    return filtfilt(window, 1, mfcc.T, axis=0)	

# Отримання файлів та приведення до однакових розмірів
def get_files(audio_path_1,audio_path_2,sr):
    y1,sr1 = librosa.load(audio_path_1,sr=sr)
    y2,sr2 = librosa.load(audio_path_2,sr=sr)
    duration_1 = librosa.get_duration(y=y1, sr=sr)
    duration_2 = librosa.get_duration(y=y2, sr=sr)
    target_duration = (duration_1 + duration_2) / 2
    y1_stretched = librosa.effects.time_stretch(y1, rate=target_duration/duration_2)
    y2_stretched = librosa.effects.time_stretch(y2, rate=target_duration/duration_1)

    return y1_stretched,y2_stretched,sr

def compute_similarity(y1_stretched,y2_stretched,sr):
# Визначення параметрів		
    frame_size = 0.05  # розмір фрейму в секундах		
    frame_stride = 0.03  # зсув фреймів в секундах		
		
# Обчислення розміру фрейма та зсуву в відліках		
    frame_length = int(sr * frame_size)		
    frame_step = int(sr * frame_stride)		
    mfccs_a = librosa.feature.mfcc(y=y1_stretched, sr=sr, n_mfcc=7, hop_length=frame_step)	
    mfccs_b = librosa.feature.mfcc(y=y2_stretched, sr=sr, n_mfcc=7, hop_length=frame_step)	

    # mfcc_ref: MFCC коефіцієнти для референсного сигналу (еталон)						
    mfcc_ref = smooth_mfcc(mfccs_a)					
# mfcc_tgt: MFCC коефіцієнти для цільового сигналу (вимірюваний)						
    mfcc_tgt = smooth_mfcc(mfccs_b)	
						
    distances = []						
                            
    for frm in range(len(mfcc_tgt)):				
        d = np.linalg.norm(mfcc_tgt[frm] - mfcc_ref[frm])				
        distances.append(d)				
                
    # mean_distance: середня відстань між всіма фреймами цільового та референсного сигналів						
    mean_distance = np.mean(distances)						
    return mean_distance     