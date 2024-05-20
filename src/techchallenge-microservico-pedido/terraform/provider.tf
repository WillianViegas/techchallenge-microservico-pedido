terraform {
  backend "s3" {
    bucket = "terraform-tfstates-totem-eks-pedido"
    key    = "totemLanchoneteEKS/terraform.tfstate"
    region = "us-east-1"
  }
}