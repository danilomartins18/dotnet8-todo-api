# Apply Metrics Server
kubectl apply -f .\metrics\metrics-server.yaml

# Apply Namespace
kubectl apply -f .\namespace.yaml

# Mongo
# Apply PVC
kubectl apply -f .\mongodb\mongo-pvc.yaml
# Apply deployment
kubectl apply -f .\mongodb\mongo-deployment.yaml
# Apply service
kubectl apply -f .\mongodb\mongo-service.yaml

# Aguardar alguns segundos para o deployment do MongoDB ser criado
Start-Sleep -Seconds 10

# Aplicar o serviço do MongoDB
kubectl apply -f mongodb-service.yaml

# Aguardar alguns segundos para o serviço do MongoDB ser criado
Start-Sleep -Seconds 10

# ToDo API
# Apply 
kubectl apply -f .\app\deployment.yaml
