import tensorflow as tf
from tensorflow.keras.models import Sequential, load_model
from tensorflow.keras.layers import Dense, Dropout, GlobalAveragePooling2D
from tensorflow.keras.preprocessing.image import ImageDataGenerator
from tensorflow.keras.applications import MobileNetV2
from tensorflow.keras.optimizers import Adam
import os
import numpy as np
from sklearn.metrics import confusion_matrix, f1_score, classification_report
import seaborn as sns
import matplotlib.pyplot as plt

# Model kaydedilecek dosya yolu
model_save_path = "garbage_classification_model.h5"

# Veri seti yolları (her iki durumda da tanımlı olmalı)
data_dir = r'C:/Users/hp/Downloads/archive/Garbage_classification'
train_dir = os.path.join(data_dir, 'train')
test_dir = os.path.join(data_dir, 'test')

# Eğer model dosyası varsa, modeli yükle; yoksa eğit
if os.path.exists(model_save_path):
    print("Eğitilmiş model yükleniyor...")
    model = load_model(model_save_path)
else:
    print("Model eğitiliyor...")

    # Veri artırma
    data_gen = ImageDataGenerator(
        rescale=1.0 / 255,
        rotation_range=30,
        width_shift_range=0.2,
        height_shift_range=0.2,
        shear_range=0.2,
        zoom_range=0.2,
        horizontal_flip=True,
        fill_mode="nearest",
        validation_split=0.2
    )

    train_gen = data_gen.flow_from_directory(
        train_dir,
        target_size=(224, 224),
        batch_size=32,
        class_mode="categorical",
        subset="training",
        shuffle=True
    )

    val_gen = data_gen.flow_from_directory(
        train_dir,
        target_size=(224, 224),
        batch_size=32,
        class_mode="categorical",
        subset="validation"
    )

    # Modelin oluşturulması
    base_model = MobileNetV2(weights="imagenet", include_top=False, input_shape=(224, 224, 3))
    base_model.trainable = False  # Önceden eğitilmiş ağırlıkları dondur

    model = Sequential([
        base_model,
        GlobalAveragePooling2D(),
        Dense(512, activation="relu"),
        Dropout(0.5),
        Dense(6, activation="softmax")
    ])

    # Modelin derlenmesi
    learning_rate = 0.00005
    optimizer = Adam(learning_rate=learning_rate)
    model.compile(optimizer=optimizer, loss="categorical_crossentropy", metrics=["accuracy"])

    # Modelin eğitilmesi
    history = model.fit(
        train_gen,
        validation_data=val_gen,
        epochs=50,  # Epoch sayısı artırıldı
        steps_per_epoch=len(train_gen),
        validation_steps=len(val_gen),
        verbose=1
    )

    # Modeli kaydet
    model.save(model_save_path)
    print(f"Model {model_save_path} dosyasına kaydedildi.")

# Modelin test edilmesi
test_gen = ImageDataGenerator(rescale=1.0 / 255).flow_from_directory(
    test_dir,
    target_size=(224, 224),
    batch_size=32,
    class_mode="categorical",
    shuffle=False
)

# Modelin test verisi üzerindeki tahminleri
predictions = model.predict(test_gen, verbose=1)
predicted_classes = np.argmax(predictions, axis=1)

# Gerçek etiketler
true_classes = test_gen.classes

# Confusion Matrix hesaplama
cm = confusion_matrix(true_classes, predicted_classes)

# F1 score hesaplama
f1 = f1_score(true_classes, predicted_classes, average='weighted')

# Confusion Matrix'i görselleştirme
plt.figure(figsize=(10, 8))
sns.heatmap(cm, annot=True, fmt="d", cmap="Blues", xticklabels=test_gen.class_indices.keys(), yticklabels=test_gen.class_indices.keys())
plt.xlabel('Predicted')
plt.ylabel('True')
plt.title('Confusion Matrix')
plt.show()

# F1 score'u yazdırma
print(f"F1 Score (Weighted): {f1:.4f}")

# Classification Report (Precision, Recall, F1 score)
report = classification_report(true_classes, predicted_classes, target_names=test_gen.class_indices.keys())
print("Classification Report:\n", report)

# Test doğruluğu
test_loss, test_accuracy = model.evaluate(test_gen, verbose=1)
print(f"Test Doğruluğu: {test_accuracy * 100:.2f}%")
