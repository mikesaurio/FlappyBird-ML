# 🐦 Unity Flappy Bird - Reinforcement Learning

Este proyecto es una implementación del clásico juego **Flappy Bird** desarrollado en **Unity**, diseñado para ser entrenado mediante **Aprendizaje por Refuerzo (Reinforcement Learning)** utilizando el toolkit de **Unity ML-Agents**.

El objetivo es que el agente (el pájaro) aprenda de forma autónoma a maximizar su puntuación atravesando la mayor cantidad de tuberías posible sin colisionar.

---

## 🚀 Características
* **Motor:** Unity 2022.3+ (o tu versión actual).
* **Algoritmo:** PPO (Proximal Policy Optimization).
* **Entorno:** ML-Agents v2.0+.
* **Hardware:** Compatible con aceleración **CUDA** para entrenamiento rápido por GPU.

---

## 🛠️ Requisitos Previos

Antes de comenzar, asegúrate de tener instalado:
1. **Python 3.8+**
2. **Unity Hub** y el Editor de Unity.
3. **VSCode**
4. **Controladores de NVIDIA** (si planeas usar CUDA).
**Nota:** En caso de no usar GPUs se puede ejecutar pero quiza convenga modificar el archivo flappy_config.yaml
---

## 📦 Instalación y Configuración

1. **Clonar el repositorio:**
```bash
    $git clone https://github.com/mikesaurio/MachineLearning.git

    $cd flappy-bird-ml
```

2. **Crear y activar el entorno virtual (venv):**
```$
    python -m venv venv

    # En Windows:
    $.\venv\Scripts\activate
    
    # En Linux/Mac:
    $source venv/bin/activate
```

3. **Instalar dependencias:**

```
    $pip install -r requirements.txt
```

---

## 🧠 Entrenamiento del Agente

Para iniciar el proceso de entrenamiento utilizando la configuración optimizada y aceleración por GPU, ejecuta el siguiente comando en tu terminal:
```
    $mlagents-learn config/flappy_config.yaml --run-id=flappy_run1 --torch-device cuda
```
**Una vez te lo indique la terminal da play desde unity.**

### Parámetros del comando:

config/flappy_config.yaml: Archivo de configuración con los hiperparámetros del modelo.

--run-id: Identificador único para esta sesión de entrenamiento.

--torch-device cuda: Fuerza el uso de la tarjeta gráfica para el cálculo de los tensores.

**Nota:** Una vez ejecutado el comando, pulsa el botón Play en el Editor de Unity para que comience la simulación.


## 📊 Configuración del Cerebro (Brain)

El agente toma decisiones basadas en las siguientes Observaciones:
* Posición vertical del pájaro ($y$).
* Velocidad lineal del pájaro.
* Distancia horizontal a la siguiente tubería.
* Distancia vertical al espacio libre entre tuberías.

### Sistema de Recompensas:

* +1.0 por pasar exitosamente por una tubería
* +0.001 por cada frame de supervivencia (recompensa existencial).
* (0.1f - distanceToCenter) * 0.01f Por estar cerca del centro del juego
* -1.0 por colisionar con el suelo o salir del juego.
* -1.0 por tocar un tubo.

## 🤝 Contribuciones¡Las sugerencias son bienvenidas!

Si tienes ideas para mejorar la función de recompensa o los hiperparámetros, no dudes en abrir un Issue o un Pull Request.